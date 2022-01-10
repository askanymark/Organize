using Organize.Shared.Entities;

namespace Organize.WASM.ItemEdit;

public class ItemEditService
{
    public EventHandler<ItemEditEventArgs> EditItemChanged;

    private BaseItem _item;

    public BaseItem Item
    {
        get { return _item; }
        set
        {
            if (_item == value) return;

            _item = value;
            var args = new ItemEditEventArgs();
            args.Item = _item;
            OnEditItemChanged(args);
        }
    }

    protected virtual void OnEditItemChanged(ItemEditEventArgs args)
    {
        EventHandler<ItemEditEventArgs> handler = EditItemChanged;

        if (handler != null) handler(this, args);
    }
}