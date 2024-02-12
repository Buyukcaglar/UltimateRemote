using Microsoft.JSInterop;

namespace UltimateRemote.Extensions;
internal static class JsRuntimeExtensions
{
    public static ValueTask BlockPage(this IJSRuntime jsRuntime, string message)
        => jsRuntime.InvokeVoidAsync("blockPage", message);

    public static ValueTask BlockPagePopup(this IJSRuntime jsRuntime, string message, string closeButtonIcon = "x-square", string closeButtonLabel = "Close", bool alignTextCenter = false)
        => jsRuntime.InvokeVoidAsync("blockPagePopup", message, closeButtonIcon, closeButtonLabel, !alignTextCenter ? "justify" : "center");

    public static ValueTask UpdateBlockPageMessage(this IJSRuntime jsRuntime, string message)
        => jsRuntime.InvokeVoidAsync("updateBlockPageMessage", message);

    public static ValueTask UnBlock(this IJSRuntime jsRuntime)
        => jsRuntime.InvokeVoidAsync("unBlock");
    
    public static ValueTask SetContentHeight(this IJSRuntime jsRuntime)
        => jsRuntime.InvokeVoidAsync("setContentHeight");

    public static ValueTask SetCardActions(this IJSRuntime jsRuntime)
        => jsRuntime.InvokeVoidAsync("setCardActions");

    public static ValueTask ScrollToElementWithId(this IJSRuntime jsRuntime, string id)
        => jsRuntime.InvokeVoidAsync("scrollToId", id);

}
