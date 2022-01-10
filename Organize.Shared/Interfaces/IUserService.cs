using Organize.Shared.Entities;

namespace Organize.Shared.Interfaces;

public interface IUserService
{
    User currentUser { get; set; }
}