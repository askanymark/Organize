using Microsoft.AspNetCore.Components;
using Organize.Shared.Enums;
using Organize.Shared.Interfaces;
using Organize.WASM.ItemEdit;
using UI.Dropdown;

namespace Organize.WASM.Pages;

public partial class ItemsOverview : ComponentBase
{
    // [Inject]
    // private ItemEditService _editService { get; set; }

    [Inject] private IUserItemManager _userItemManager { get; set; }

    [Inject] private IUserService _userService { get; set; }

    private bool _showEdit;

    private DropdownItem<ItemType> _selectedType { get; set; }

    private IList<DropdownItem<ItemType>> _dropdownTypes { get; set; }

    [Parameter] public string Type { get; set; }

    [Parameter] public int? Id { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        // _editService.EditItemChanged += HandleEditItemChanged;
        _dropdownTypes = new List<DropdownItem<ItemType>>();
        
        var item = new DropdownItem<ItemType>
        {
            ItemObject = ItemType.Text,
            DisplayText = "Text"
        };
        _dropdownTypes.Add(item);

        item = new DropdownItem<ItemType>
        {
            ItemObject = ItemType.Url,
            DisplayText = "Url"
        };
        _dropdownTypes.Add(item);

        item = new DropdownItem<ItemType>
        {
            ItemObject = ItemType.Parent,
            DisplayText = "Parent"
        };
        _dropdownTypes.Add(item);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Id != null && Enum.TryParse(typeof(ItemType), Type, out _))
        {
            _showEdit = true;
        }
        else
        {
            _showEdit = false;
        }
    }

    private void HandleEditItemChanged(object sender, ItemEditEventArgs args)
    {
        _showEdit = args.Item != null;
        StateHasChanged();
    }

    private async Task AddNewAsync()
    {
        if (_selectedType == null) return;

        await _userItemManager.CreateNewUserItemAndAddItToUserAsync(_userService.currentUser, _selectedType.ItemObject);
    }
}