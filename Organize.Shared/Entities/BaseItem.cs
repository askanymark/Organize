using Organize.Shared.Enums;

namespace Organize.Shared.Entities;

public class BaseItem : BaseEntity
{
    public int ParentId
    {
        get => _parentId;
        set => SetProperty(ref _parentId, value);
    }

    private int _parentId;

    public ItemType Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }

    private ItemType _type;

    public int Position
    {
        get => _position;
        set => SetProperty(ref _position, value);
    }

    private int _position;

    public bool IsDone
    {
        get => _isDone;
        set => SetProperty(ref _isDone, value);
    }

    private bool _isDone;

    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _title;
}