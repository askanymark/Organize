using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.Business;

public class UserManager : IUserManager
{
    private readonly IUserDataAccess _dataAccess;

    public UserManager(IUserDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    public async Task<User> SignInAsync(User user)
    {
        return await _dataAccess.AuthenticateAsync(user);
    }

    public async Task RegisterAsync(User user)
    {
        var isAlreadyAvailable = await _dataAccess.IsUserWithNameAvailableAsync(user);

        if (isAlreadyAvailable) throw new Exception("Username already exists");

        await _dataAccess.InsertUserAsync(user);
    }
}