using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.TestFake;

public class UserManagerFake : IUserManager
{
    public Task<User> SignInAsync(User user)
    {
        Console.WriteLine("Hello from fake!");
        return Task.FromResult(user);
    }

    public Task RegisterAsync(User user)
    {
        return Task.FromResult(new User());
    }
}