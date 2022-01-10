using Organize.Shared.Entities;

namespace Organize.WASM.ItemEdit;

public class ItemEditEventArgs : EventArgs
{
    public BaseItem Item { get; set; }
}