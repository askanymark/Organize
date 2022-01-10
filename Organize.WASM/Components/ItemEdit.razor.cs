using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Organize.Business;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.Shared.Interfaces;
using Organize.WASM.ItemEdit;

namespace Organize.WASM.Components;

public partial class ItemEdit : ComponentBase, IDisposable
{
    // [Inject] private ItemEditService _editService { get; set; }

    private BaseItem Item { get; set; } = new();

    private int TotalNumber { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }
    
    [Inject]
    private IUserService _userService { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        // _editService.EditItemChanged += HandleItemChanged;
        // Item = _editService.Item;
        SetDataFromUri();
    }

    private void SetDataFromUri()
    {
        var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

        var segments = uri.Segments.Length;
        if (segments > 2 &&
            Enum.TryParse(typeof(ItemType), uri.Segments[segments - 2].Trim('/'), out var type) &&
            int.TryParse(uri.Segments[segments - 1], out var id))
        {
            var userItem =
                _userService.currentUser.Items.SingleOrDefault(item => item.Type == (ItemType) type && item.Id == id);

            if (userItem == null)
            {
                _navigationManager.LocationChanged -= HandleLocationChanged;
                _navigationManager.NavigateTo("/items");
            }
            else
            {
                Item = userItem;
                _navigationManager.LocationChanged += HandleLocationChanged;
                StateHasChanged();
            }
        }
    }

    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        SetDataFromUri();
    }


    // private void HandleItemChanged(object? sender, ItemEditEventArgs e)
    // {
    //     Item = e.Item;
    //     StateHasChanged();
    // }
    public void Dispose()
    {
        _navigationManager.LocationChanged -= HandleLocationChanged;
    }
}