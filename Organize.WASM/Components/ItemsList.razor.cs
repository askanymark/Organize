using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Components;
using Organize.Shared.Entities;
using Organize.Shared.Interfaces;
using Organize.WASM.ItemEdit;

namespace Organize.WASM.Components;

public partial class ItemsList : ComponentBase, IDisposable
{
    [Inject] private IUserService _userService { get; set; }

    [Inject] private ItemEditService _editService { get; set; }
    
    [Inject] private NavigationManager _navigationManager { get; set; }
    
    [Inject] private IUserItemManager _itemManager { get; set; }

    protected ObservableCollection<BaseItem> _items { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await _itemManager.RetrieveAllUserItemsOfUserAndSetToUserAsync(_userService.currentUser);
        
        _items = _userService.currentUser.Items;
        _items.CollectionChanged += HandleUserItemsCollectionChanged;
    }

    private void HandleUserItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        StateHasChanged();
    }

    private void OnBackgroundClicked()
    {
        // _editService.Item = null;
        _navigationManager.NavigateTo("/items");
    }

    public void Dispose()
    {
        _items.CollectionChanged -= HandleUserItemsCollectionChanged;
    }
}