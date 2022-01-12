using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.Shared.Interfaces;
using UI.Dropdown;

namespace Organize.WASM.Pages;

public class SignUpBase : ComponentBase
{
    [Inject] private IUserManager _userManager { get; set; }

    [Inject] private NavigationManager _navigationManager { get; set; }

    protected User User { get; set; } = new();

    protected EditContext? EditContext { get; set; }

    protected IList<DropdownItem<Gender>> genderOptions { get; set; } = new List<DropdownItem<Gender>>();

    protected DropdownItem<Gender> selectedGender { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        EditContext = new EditContext(User);
        var male = new DropdownItem<Gender>
        {
            ItemObject = Gender.Male,
            DisplayText = "Male"
        };
        var female = new DropdownItem<Gender>
        {
            ItemObject = Gender.Female,
            DisplayText = "Female"
        };
        var other = new DropdownItem<Gender>
        {
            ItemObject = Gender.Other,
            DisplayText = "Other"
        };

        genderOptions.Add(male);
        genderOptions.Add(female);
        genderOptions.Add(other);

        selectedGender = female;

        ParseUri();
    }

    protected string? GetError(Expression<Func<object>> fu)
    {
        return EditContext?.GetValidationMessages(fu).FirstOrDefault();
    }

    private void ParseUri()
    {
        var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("username", out var stringValues))
        {
            User.UserName = stringValues;
        }
    }

    protected async void OnValidSubmit()
    {
        await _userManager.RegisterAsync(User);
        _navigationManager.NavigateTo("signin");
    }
}