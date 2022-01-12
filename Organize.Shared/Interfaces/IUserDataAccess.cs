using Organize.Shared.Entities;

namespace Organize.Shared.Interfaces;

public interface IUserDataAccess
{
    Task<bool> IsUserWithNameAvailableAsync(User user);
    Task InsertUserAsync(User user);
    Task<User> AuthenticateAsync(User user);
}