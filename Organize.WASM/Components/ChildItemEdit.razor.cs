using Microsoft.AspNetCore.Components;
using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.WASM.Components;

public partial class ChildItemEdit
{
    [Inject]
    private IUserItemManager _itemManager { get; set; }
    
    [Parameter]
    public ParentItem ParentItem { get; set; }

    private async Task AddNewChildToParentAsync()
    {
        await _itemManager.CreateNewChildItemAndAddItToParentAsync(ParentItem);
    }
}