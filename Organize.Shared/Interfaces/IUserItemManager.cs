using Organize.Shared.Entities;
using Organize.Shared.Enums;

namespace Organize.Shared.Interfaces;

public interface IUserItemManager
{
    Task<ChildItem> CreateNewChildItemAndAddItToParentAsync(ParentItem parentItem);

    Task<BaseItem> CreateNewUserItemAndAddItToUserAsync(User user, ItemType type);
}