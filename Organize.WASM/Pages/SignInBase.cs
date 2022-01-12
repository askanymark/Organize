using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Organize.Business;
using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.WASM.Pages;

public class SignInBase : ComponentBase
{
    [Inject] private IUserService _userService { get; set; }
    
    [Inject] private NavigationManager _navigationManager { get; set; }

    [Inject] private IUserManager _userManager { get; set; }

    protected string Day { get; set; } = DateTime.Now.DayOfWeek.ToString();

    protected User User { get; set; } = new User();

    protected async void OnSubmit()
    {
        if (!EditContext.Validate())
        {
            foreach (var message in EditContext.GetValidationMessages())
            {
                Console.WriteLine(message);
            }
            Console.WriteLine("Sign in input is invalid");
            return;
        }

        var user = await _userManager.SignInAsync(User);

        if (user != null)
        {
            _userService.currentUser = user;
            _navigationManager.NavigateTo("items");
        }
    }

    protected EditContext? EditContext { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        User = new User
        {
            FirstName = "abc",
            LastName = "abc",
            PhoneNumber = "123"
        };
        EditContext = new EditContext(User);
    }

    protected string? GetError(Expression<Func<object>> fu)
    {
        return EditContext?.GetValidationMessages(fu).FirstOrDefault();
    }
}