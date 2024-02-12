using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Layout;
public sealed partial class PageHeader
{/*
    [Inject] private BreadCrumbsService BreadCrumbsService { get; set; } = default!;

    public BreadCrumb[] BreadCrumbs { get; set; } = Array.Empty<BreadCrumb>();

    protected override void OnInitialized()
    {
        BreadCrumbsService.UpdateBreadCrumbsEvent -= OnBreadCrumbsUpdate;
        BreadCrumbsService.UpdateBreadCrumbsEvent += OnBreadCrumbsUpdate;
        base.OnInitialized();
    }

    private async void OnBreadCrumbsUpdate(object? sender, BreadCrumb[] breadCrumbs)
    {
        BreadCrumbs = breadCrumbs;
        await InvokeAsync(StateHasChanged);
    }
    */
}
