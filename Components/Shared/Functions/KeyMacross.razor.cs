using MonkeyCache.FileStore;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class KeyMacross : BaseComponent
{
    private KeyMacro[] _macros =
        Barrel.Current.Get<KeyMacro[]>(key: CacheKeys.KeyMacros) ??
        Array.Empty<KeyMacro>();

    private string _macroName = string.Empty;
    private string _macroString = string.Empty;
    private string _macroHtml = string.Empty;

    private string NameInputCss => _macros.Length > 0 ? "form-control rounded-top-0 p-1" : "form-control rounded-top-0 rounded-bottom-start p-1";

    private Task OnInputChange(Microsoft.AspNetCore.Components.ChangeEventArgs args)
    {
        var value = (string?)args.Value;

        if (string.IsNullOrWhiteSpace(value))
        {
            _macroHtml = string.Empty;
            return Task.CompletedTask;
        }
        
        _macroHtml = PETSCIICodes.GetHtmlCodesString(value.Replace("\n", "\r"));
        
        return Task.CompletedTask;
    }

    private Task AddEdit()
    {
        if (string.IsNullOrWhiteSpace(_macroString))
        {
            DisplayErrorToast(message: Strings.KeyMacros.ToastMsgMacroEmpty, Strings.KeyMacros.ToastTitleMacroEmpty);
            return Task.CompletedTask;
        }

        if (string.IsNullOrWhiteSpace(_macroName))
        {
            DisplayErrorToast(message: Strings.KeyMacros.ToastMsgNameMacroEmpty, Strings.KeyMacros.ToastTitleMacroNameEmpty);
            return Task.CompletedTask;
        }

        _macroName = _macroName.Trim();

        _macros = _macros.Add(new KeyMacro(_macroName, _macroString));
        
        PersistMacroList();
        return Task.CompletedTask;
    }

    private async Task OnItemSelected(KeyMacro macro)
    {
        _macroName = macro.Name;
        _macroString = macro.Contents;
        await OnInputChange(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = macro.Contents });
        await ExecuteMacro();
    }

    

    private Task ExecuteMacro()
        => !string.IsNullOrWhiteSpace(_macroString)
            ? CurrentDevice.ExecuteKeyboardBuffer(_macroString.Replace("\n", "\r"))
            : Task.CompletedTask;
    

    private Task Remove(int index)
    {
        _macros = _macros.RemoveAt(index);
        PersistMacroList();
        return Task.CompletedTask;
    }

    private void PersistMacroList()
    {
        Barrel.Current.Add<KeyMacro[]>(key: CacheKeys.KeyMacros, _macros, expireIn: TimeSpan.Zero);
    }
}

