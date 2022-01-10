using Microsoft.AspNetCore.Components;

namespace UI.Dropdown;

public partial class Dropdown<T> : ComponentBase
{
    [Parameter]
    public IList<DropdownItem<T>> SelectableItems { get; set; }
    
    [Parameter]
    public DropdownItem<T> SelectedItem { get; set; }
    
    [Parameter]
    public EventCallback<DropdownItem<T>> SelectedItemChanged { get; set; }

    public async void OnItemClicked(DropdownItem<T> item)
    {
        SelectedItem = item;
        StateHasChanged();
        await SelectedItemChanged.InvokeAsync(item);
    }
}