using System.Collections.ObjectModel;
using Organize.Shared.Entities;
using Organize.Shared.Enums;

namespace Organize.TestFake;

public class TestData
{
    public static User testUser { get; set; }

    public static void CreateTestUser()
    {
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

        var textItem = new TextItem
        {
            ParentId = user.Id,
            Id = 1,
            Title = "Buy cheese",
            SubTitle = "Edam slices & mature cheddar",
            Detail = "Slices from Morrisons and cheddar from Cathedral City",
            Type = ItemType.Text,
            Position = 1,
            IsDone = false
        };

        var urlItem = new UrlItem
        {
            ParentId = user.Id,
            Id = 2,
            Title = "Buy this beer mug",
            Url = "https://s3-us-west-2.amazonaws.com/craftbeerdotcom/wp-content/uploads/fall-beer-stein.jpg",
            Position = 2,
            Type = ItemType.Url,
            IsDone = false,
        };

        var parentItem = new ParentItem
        {
            ParentId = user.Id,
            Id = 3,
            Position = 3,
            Title = "Make birthday present",
            Type = ItemType.Parent,
            IsDone = false,
            ChildItems = new ObservableCollection<ChildItem>()
        };

        var childItem = new ChildItem
        {
            ParentId = parentItem.Id,
            Id = 4,
            Position = 1,
            Title = "Cut"
        };
        
        parentItem.ChildItems.Add(childItem);

        user.Items.Add(textItem);
        user.Items.Add(urlItem);
        user.Items.Add(parentItem);

        testUser = user;
    }
}