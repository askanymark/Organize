using Organize.Shared.Entities;

namespace Organize.Shared.Interfaces;

public interface IUserManager
{
    Task<User> SignInAsync(User user);
    Task RegisterAsync(User user);
}