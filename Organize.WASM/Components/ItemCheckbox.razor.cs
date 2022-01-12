using Microsoft.AspNetCore.Components;
using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.WASM.Components;

public partial class ItemCheckbox
{
    [Parameter] public BaseItem Item { get; set; }

    [CascadingParameter] public string ColorPrefix { get; set; }

    [Inject] private IUserItemManager _itemManager { get; set; }

    public async Task ChangeIsDone()
    {
        Item.IsDone = !Item.IsDone;
        await _itemManager.UpdateAsync(Item);
    }
}