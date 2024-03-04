using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Extensions;
internal static class BlazoredModalExtensions
{
    public static void DisplayModal<T>(this IModalService modalService, string modalTitle,
        params (string Name, object Value)[] modalParams) where T : ComponentBase
    {
        var modalParameters = new ModalParameters();
        foreach (var modalParam in modalParams)
        {
            modalParameters.Add(modalParam.Name, modalParam.Value);
        }

        modalParameters.Add("ModalTitle", modalTitle);

        var modal = modalService.Show<T>(title: "", modalParameters);

        modalParameters.Add("Self", modal);
    }

    public static T? GetModalData<T>(this ModalResult modalResult)
    {
        if (null == modalResult.Data)
            return default(T?);
        
        if (modalResult.Data is T data)
            return data;
        try
        {
            return (T)Convert.ChangeType(modalResult.Data, typeof(T));
        }
        catch (InvalidCastException)
        {
            return default(T?);
        }
    }
}
