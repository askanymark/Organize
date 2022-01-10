using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.Business;

public class UserService: IUserService
{
    public User currentUser { get; set; }
}