using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class BasicFunction : BaseComponent
{
    [Parameter] public LayoutItemType FuncType { get; set; } = default!;

    [Parameter] public string? FunctionCardAlignSelfStart { get; set; } = "align-self-start";
    
    private DotNetObjectReference<BasicFunction>? _dotNetRef;

    protected override async Task OnInitializedAsync()
    {
        _dotNetRef ??= DotNetObjectReference.Create(this);
        await base.OnInitializedAsync();
    }

    private string FuncIcon => FuncType switch
    {
        LayoutItemType.ResetMachine => "arrow-clockwise",
        LayoutItemType.RebootMachine => "arrow-counter-clockwise",
        _ => ""
    };

    private string FuncInfo => FuncType switch
    {
        LayoutItemType.ResetMachine => Strings.Function.Infos.ResetMachine,
        LayoutItemType.RebootMachine => Strings.Function.Infos.RebootMachine,
        _ => ""
    };

    private async Task ConfirmAction()
        => await BlockPageWithConfirm(message: $"This will {(FuncType switch
        {
            LayoutItemType.ResetMachine => "reset",
            LayoutItemType.RebootMachine => "reboot",
            _ => ""
        })} your C64. Do you want to continue?", nameof(PerformAction), _dotNetRef!, true);

    [JSInvokable]
    public Task PerformAction() => FuncType switch
    {
        LayoutItemType.ResetMachine => CurrentDevice.ResetMachine(),
        LayoutItemType.RebootMachine => CurrentDevice.RebootMachine(),
        _ => Task.CompletedTask
    };


    public override void Dispose()
    {
        _dotNetRef?.Dispose();
        base.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        _dotNetRef?.Dispose();
        await base.DisposeAsync();
    }
}
