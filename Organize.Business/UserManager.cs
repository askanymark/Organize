using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.Business;

public class UserManager : IUserManager
{
    public async Task<User> SignInAsync(User user)
    {
        // await Task.Delay(10000);
        Console.WriteLine("Hi from user manager");
        return await Task.FromResult(new User());
    }

    public async Task RegisterAsync(User user)
    {
        await Task.FromResult(true);
    }
}