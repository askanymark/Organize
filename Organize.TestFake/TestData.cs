using System.Collections.ObjectModel;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.Shared.Interfaces;

namespace Organize.TestFake;

public class TestData
{
    public static User testUser { get; set; }

    public static void CreateTestUser(IUserItemManager itemManager = null)
    {
        TextItem textItem = null;
        UrlItem urlItem = null;
        ParentItem parentItem = null;
        ChildItem childItem = null;
        var user = new User
        {
            Id = 1,
            UserName = "askanymark",
            FirstName = "Marks",
            LastName = "Paskannijs",
            Password = "test",
            Gender = Gender.Male,
            Items = new ObservableCollection<BaseItem>()
        };

        if (itemManager != null)
        {
            textItem = (TextItem) itemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemType.Text).Result;
        }
        else
        {
            textItem = new TextItem();
            user.Items.Add(textItem);
        }

        textItem.ParentId = user.Id;
        textItem.Id = 1;
        textItem.Title = "Buy cheese";
        textItem.SubTitle = "Edam slices & mature cheddar";
        textItem.Detail = "Slices from Morrisons and cheddar from, Cathedral City";
        textItem.Type = ItemType.Text;
        textItem.Position = 1;
        textItem.IsDone = false;

        if (itemManager != null)
        {
            urlItem = (UrlItem) itemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemType.Url).Result;
        }
        else
        {
            urlItem = new UrlItem();
            user.Items.Add(urlItem);
        }

        urlItem.ParentId = user.Id;
        urlItem.Id = 2;
        urlItem.Title = "But this beer mug";
        urlItem.Url = "https://s3-us-west-2.amazonaws.com/craftbeerdotcom/wp-content/uploads/fall-beer-stein.jpg";
        urlItem.Position = 2;
        urlItem.Type = ItemType.Url;
        urlItem.IsDone = false;

        if (itemManager != null)
        {
            parentItem = (ParentItem) itemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemType.Parent).Result;
        }
        else
        {
            parentItem = new ParentItem();
            user.Items.Add(parentItem);
        }

        parentItem.ParentId = user.Id;
        parentItem.Id = 3;
        parentItem.Position = 3;
        parentItem.Title = "Make birthday present";
        parentItem.Type = ItemType.Parent;
        parentItem.IsDone = false;
        parentItem.ChildItems = new ObservableCollection<ChildItem>();

        if (itemManager != null)
        {
            childItem = (ChildItem) itemManager.CreateNewChildItemAndAddItToParentAsync(parentItem).Result;
            user.Items.Clear();
        }
        else
        {
            childItem = new ChildItem();
            parentItem.ChildItems.Add(childItem);
        }

        childItem.ParentId = parentItem.Id;
        childItem.Id = 4;
        childItem.Position = 1;
        childItem.Title = "Cut";

        testUser = user;
    }
}