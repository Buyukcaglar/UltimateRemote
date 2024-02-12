using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UltimateRemote.Components.Shared;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.BasicEditor)]
public sealed partial class BasicEditor : BaseComponent, IDisposable
{
    private IJSObjectReference? _js;
    private DotNetObjectReference<BasicEditor>? _dotNetRef;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
            await InitEditor();
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task InitEditor()
    {
        _js ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Pages/BasicEditor.razor.js");
        _dotNetRef ??= DotNetObjectReference.Create(this);
        await _js.InvokeVoidAsync("initEditor", _dotNetRef);
        //await _js.InvokeVoidAsync("initEditor");
    }

    private async Task ParseBasic()
    {
        _js ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Pages/BasicEditor.razor.js");
        var parseResult =  await _js.InvokeAsync<ParseBasicResult?>("parseBASIC");
        
        if (parseResult is {Valid: true})
        {
            await CurrentDevice.WriteMemory(parseResult.Address1!, parseResult.Data1!).ExecOnSuccess(() =>
                CurrentDevice.WriteMemory(parseResult.Address2!, parseResult.Data2!)
                    .ExecOnSuccess(() =>
                    {
                        DisplaySuccessToast(message: Strings.BasicEditor.ToastMsgProgramUploaded,
                            title: Strings.BasicEditor.ToastTitleProgramUploaded);
                        return Task.CompletedTask;
                    })
            );
        }
    }

    public override void Dispose()
    {
        if (_js is IDisposable jsDisposable)
            jsDisposable.Dispose();
        else if (_js != null)
            _ = _js.DisposeAsync().AsTask();
        _dotNetRef?.Dispose();
        
        base.Dispose();
    }
}

record ParseBasicResult(string? Address1, byte[]? Data1, string? Address2, string? Data2)
{
    public bool Valid = !string.IsNullOrWhiteSpace(Address1) && Data1 is { Length: > 0 } &&
                        !string.IsNullOrWhiteSpace(Address2) && !string.IsNullOrWhiteSpace(Data2);
};