namespace Organize.Shared.Entities;

public class TextItem : BaseItem
{
    public string? SubTitle
    {
        get => _subTitle;
        set => SetProperty(ref _subTitle, value);
    }

    private string _subTitle;
    private string? _detail;

    public string? Detail
    {
        get => _detail;
        set => SetProperty(ref _detail, value);
    }
}