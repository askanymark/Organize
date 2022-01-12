using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private readonly IPersistenceService _persistenceService;

    public UserDataAccess(IPersistenceService persistenceService)
    {
        _persistenceService = persistenceService;
    }

    public async Task<bool> IsUserWithNameAvailableAsync(User user)
    {
        var users = await _persistenceService.GetAsync<User>(u => u.UserName == user.UserName);

        return users.FirstOrDefault() != null;
    }

    public async Task InsertUserAsync(User user)
    {
        await _persistenceService.InsertAsync(user);
    }

    public async Task<User> AuthenticateAsync(User user)
    {
        var users = await _persistenceService.GetAsync<User>(u =>
            u.UserName == user.UserName && u.Password == user.Password);

        return users.FirstOrDefault();
    }
}