using Microsoft.AspNetCore.Components;
using Organize.Shared.Interfaces;

namespace Organize.WASM.Pages;

public partial class Settings : ComponentBase
{
    [Inject] private IUserItemManager _itemManager { get; set; }

    [Inject] private IUserService _userService { get; set; }

    private async void DeleteAllDone()
    {
        await _itemManager.DeleteAllDoneAsync(_userService.currentUser);
    }
}