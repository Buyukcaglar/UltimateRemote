using Microsoft.AspNetCore.Components;
using System.Timers;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class Jukebox : BaseComponent
{
    [Inject] private JukeboxService JukeboxService { get; set; } = default!;

    [Inject] private EventService EventService { get; set; } = default!;

    [Parameter, EditorRequired] public LayoutItem Item { get; set; } = default!;

    private string _componentId = Guid.NewGuid().ToString("N");

    private System.Timers.Timer _timer = default!;
    private const double TimerIntervalInMs = 500;

    private bool _searchEnabled;
    private bool _repeat;
    private bool _shuffle;
    private bool _playing;
    
    private SidFileInfo[] _tracks = [];
    private List<int> _playHistory = [];
    private SidFileInfo? _currentTrack;
    private TimeSpan _songDuration;
    private int _playPercentage;
    private string _playedSoFar = "00:00";
    private string _remainingPlay = "00:00";
    private double _singlePercentValue;

    private string _infoText = "";
    private string _cardTitle = "";
    
    private string CardIconCss => !_playing ? "vinyl-record" : "vinyl-record spin-cw-slow";

    private string ShuffleButtonCss => _tracks.Length > 1 ? (_shuffle ? "py-0 btn-highlight" : "py-0") : "d-none";
    private string RepeatButtonCss => _repeat ? "py-0 btn-highlight" : "py-0";

    //private string NowPlaying => !string.IsNullOrWhiteSpace(_currentTrack?.FilePath) && _playing
    //    ? !PrefsMgr.DisplayFilepathWhileHVSCPlay ? _currentTrack.FormattedFileName! : _currentTrack.FilePath!
    //    : "STOPPED";

    private string? NowPlaying => !string.IsNullOrWhiteSpace(_currentTrack?.FilePath) && _playing
        ? !PrefsMgr.DisplayFilepathWhileHVSCPlay
            ? Item.Type == LayoutItemType.HVSCSIDFile ? Item.Name : _currentTrack.FormattedFileName
            : _currentTrack.FilePath
        : "STOPPED";

    private string? NowPlayingLabelCss => _playing ? "text-nowrap jukebox-marquee-bounce" : null;
    
    private string _tracksLabel = $"";
    private Func<SidFileInfo, string> IconCssFunc => (item) => item == _currentTrack ? "play ph-duotone" : "";

    protected override void OnInitialized()
    {
        Init();
        base.OnInitialized();
    }

    private void Init()
    {
        EventService.SidPlayInitiatedEvent -= OnSidPlayInitiate;
        EventService.SidPlayInitiatedEvent += OnSidPlayInitiate;

        if (Item.Type == LayoutItemType.JukeboxPlaylist)
        {
            var playlistId = Item.GetData<string>();

            if (!string.IsNullOrWhiteSpace(playlistId))
            {
                // Ensure that we have the updated version of the playlist
                var playlist = JukeboxService.GetPlaylist(playlistId);
            
                if (playlist is { ItemCount: > 0 })
                {
                    _cardTitle = playlist.Name;
                    BuildTrackList(playlist);
                    _infoText = Strings.Jukebox.InfoPlaylist(playlist.Name, playlist.ItemCount, _tracks.Length);
                    _tracksLabel = Strings.Jukebox.TracksLabelPlaylist(playlist.Name, playlist.ItemCount, _tracks.Length);
                    _searchEnabled = playlist.Items.Count > 10;
                }
            }
        }

        if (Item.Type == LayoutItemType.HVSCSIDFile)
        {
            var sidFile = Item.GetData<SidFileInfo>();
            if (null != sidFile)
            {
                _cardTitle = !string.IsNullOrWhiteSpace(Item.Name)
                    ? Item.Name
                    : StringSearchExtensions.ConvertToSearchableString(sidFile.FileNameWithoutExtension())
                        .ToUpperInvariant();
                BuildTrackList(new JukeboxPlaylist() { Items = [sidFile], Name = "" });
                _infoText = Strings.Jukebox.InfoSidFile(_cardTitle, sidFile.NumberOfSongs, sidFile.TotalLength.ToString(@"mm\:ss"));
                _tracksLabel = Strings.Jukebox.TracksLabelSidFile(_cardTitle, _tracks.Length);
            }
        }

        InitTimer();

    }

    private async void OnSidPlayInitiate(object? sender, string componentId)
    {
        if (componentId != _componentId)
        {
            await Reset();
        }
    }

    private void BuildTrackList(JukeboxPlaylist playlist)
    {
        var trackList = new List<SidFileInfo>();
        foreach (var sidFile in playlist.Items)
        {
            var formattedFileName = StringSearchExtensions.ConvertToSearchableString(sidFile.FileNameWithoutExtension()).ToUpperInvariant();
            if (sidFile.NumberOfSongs > 1)
            {
                trackList.AddRange(sidFile.SongLengths.Select((songLength, index) =>
                    new SidFileInfo(sidFile.FilePath, sidFile.HashMD5, "", 1, songLength, [songLength])
                    {
                        FormattedFileName = $"{formattedFileName} ({index + 1}/{sidFile.SongLengths.Length})" ,
                        SongNumber = index + 1
                    }));
            }
            else
            {
                // This with keyword is intriguing ...
                trackList.Add(sidFile with {
                    SearchContent = "",
                    FormattedFileName = formattedFileName
                });
            }
        }
        _tracks = [.. trackList];
    }

    private void InitTimer()
    {
        _timer = new System.Timers.Timer(TimerIntervalInMs);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
    }

    private async Task Stop()
    {
        var wasPlaying = _playing;
        
        await Reset();
        
        if(wasPlaying)
            await CurrentDevice.ResetMachine();
    }

    private Task Reset()
    {
        _timer.Stop();
        _playing = false;
        _currentTrack = null;
        _playHistory.Clear();

        _playPercentage = 0;
        _playedSoFar = "00:00";
        _remainingPlay = "00:00";
        _singlePercentValue = 0;

        return InvokeAsync(StateHasChanged);
    }

    private Task PlayNext(bool skipToNext)
    {
        if (_tracks.Length == 0 || (_playing && !skipToNext))
            return Task.CompletedTask;

        var nextTune = default(SidFileInfo?);

        if (_shuffle)
        {
            var unPlayedItems = _tracks.Where((_, idx) => !_playHistory.Contains(idx)).ToArray();
            if (unPlayedItems.Length == 0 && _repeat)
                unPlayedItems = _tracks;
            nextTune = Random.Shared.GetItems(unPlayedItems, 1)[0];
        }
        else
        {
            var nextTuneIndex = null != _currentTrack ? _tracks.ToList().IndexOf(_currentTrack) + 1 : 0;

            if (nextTuneIndex >= _tracks.Length && _repeat)
                nextTuneIndex = 0;

            if (nextTuneIndex < _tracks.Length)
                nextTune = _tracks[nextTuneIndex];
        }

        if (null != _currentTrack)
            _playHistory.Add(_tracks.ToList().IndexOf(_currentTrack));

        if (default == nextTune)
            return Stop();

        _currentTrack = nextTune;

        return Play();

    }

    private Task PlayPrevious()
    {
        if (null == _currentTrack)
            return Task.CompletedTask;

        if (_playHistory.Count == 0)
            return Task.CompletedTask;

        var previousTune = _tracks[_playHistory.Last()];
        _playHistory.RemoveAt(_playHistory.Count - 1);
        _currentTrack = previousTune;

        return Play();
    }

    private Task PlaySelected(SidFileInfo selectedItem)
    {
        if (null != _currentTrack)
            _playHistory.Add(_tracks.ToList().IndexOf(_currentTrack));
        _currentTrack = selectedItem;
        return Play();
    }

    private async Task Play()
    {
        _playing = false;

        if (null == _currentTrack)
            return;

        var sidContentBytes = JukeboxService.GetSidFileContents(_currentTrack);

        if (sidContentBytes is not { Length: > 0 })
        {
            DisplayWarningToast(message: Strings.Jukebox.ToastMsgSidRetrieveFail, title: Strings.Jukebox.ToastTitleSidRetrieveFail);
            return;
        }

        await CurrentDevice
            .PlayUploadedSidFile(sidContentBytes, _currentTrack.FormattedFileName ?? "my-music.sid", _currentTrack.SongNumber)
            .ExecOnSuccess(() =>
            {
                EventService.SignalSidPlay(this, _componentId);
                _playing = true;
                _songDuration = _currentTrack.SongLengths[0];
                _singlePercentValue = 100/_songDuration.TotalMilliseconds;
                _timer.Start();
            });
    }
    
    private async void OnTimerElapsed(object? source, ElapsedEventArgs? e)
    {
        _songDuration = _songDuration.Subtract(TimeSpan.FromMilliseconds(TimerIntervalInMs));
        
        var played = _currentTrack?.SongLengths[0].Subtract(_songDuration);
        
        _playedSoFar = played?.ToString( @"mm\:ss") ?? "00:00";
        _remainingPlay = _songDuration.ToString( @"mm\:ss");
        _playPercentage = Convert.ToInt32((_singlePercentValue * played?.TotalMilliseconds));

        await InvokeAsync(StateHasChanged);

        if (_songDuration.TotalMilliseconds < TimerIntervalInMs)
        {
            _timer.Stop();
            await PlayNext(true);
        }
    }

    public override void Dispose()
    {
        _timer.Dispose();
        base.Dispose();
    }
}
