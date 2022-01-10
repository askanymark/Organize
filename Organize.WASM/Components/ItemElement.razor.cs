using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Organize.Shared.Entities;
using Organize.WASM.ItemEdit;

namespace Organize.WASM.Components;

public partial class ItemElement<T> : ComponentBase, IDisposable where T : BaseItem
{
    [Parameter] public RenderFragment MainFragment { get; set; }

    [Parameter] public RenderFragment DetailFragment { get; set; }

    [Parameter] public T Item { get; set; }

    [CascadingParameter] public string ColorPrefix { get; set; }

    [CascadingParameter] public int TotalNumber { get; set; }

    [Inject] private NavigationManager _navigationManager { get; set; }

    // [Inject] private ItemEditService _itemEditService { get; set; }

    public string DetailAreaId { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        DetailAreaId = $"detailArea{Item.Position}";
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender)
        {
            Item.PropertyChanged += HandleItemPropertyChanged;
        }
    }

    private void HandleItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        StateHasChanged();
    }

    private void OpenItemInEditMode()
    {
        // _itemEditService.Item = Item;
        Uri.TryCreate($"/items/{Item.Type}/{Item.Id}", UriKind.Relative, out var uri);
        _navigationManager.NavigateTo(uri.ToString());
    }

    public void Dispose()
    {
        Item.PropertyChanged -= HandleItemPropertyChanged;
    }
}