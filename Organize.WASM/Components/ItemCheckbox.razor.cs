using Microsoft.AspNetCore.Components;
using Organize.Shared.Entities;

namespace Organize.WASM.Components;

public partial class ItemCheckbox
{
    [Parameter]
    public BaseItem Item { get; set; }
    
    [CascadingParameter]
    public string ColorPrefix { get; set; }
}