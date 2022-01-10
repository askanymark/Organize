using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Organize.Shared.Enums;

namespace Organize.Shared.Entities;

public class User : BaseEntity
{
    [Required]
    [StringLength(10, ErrorMessage = "Username is too long")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "The password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone]
    public string PhoneNumber { get; set; }

    public ObservableCollection<BaseItem> Items { get; set; }

    public override string ToString()
    {
        var title = Gender switch
        {
            Gender.Male => "Mr",
            Gender.Female => "Mrs",
            _ => string.Empty
        };

        return $"{title}. {FirstName} {LastName}";
    }
}