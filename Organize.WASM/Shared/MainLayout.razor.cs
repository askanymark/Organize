using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Organize.Shared.Interfaces;

namespace Organize.WASM.Shared;

public partial class MainLayout : IDisposable
{
    private DotNetObjectReference<MainLayout> _dotNetObjectReference;

    [Inject]
    private IUserService _userService { get; set; }
    
    [Inject]
    private IJSRuntime _jsRuntime { get; set; }

    private bool _useShortNavText { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var width = await _jsRuntime.InvokeAsync<int>("blazorDimension.getWidth");
        CheckUseShortNavText(width);

        _dotNetObjectReference = DotNetObjectReference.Create(this);
        await _jsRuntime.InvokeVoidAsync("blazorResize.registerReferenceForResizeEvent", _dotNetObjectReference);
    }

    protected void SignOut()
    {
    }

    [JSInvokable]
    public static void OnResize()
    {
        
    }

    [JSInvokable]
    public void HandleResize(int width, int height)
    {
        CheckUseShortNavText(width);
    }

    private void CheckUseShortNavText(int width)
    {
        var oldValue = _useShortNavText;

        _useShortNavText = width < 700;
        
        if (oldValue != _useShortNavText) StateHasChanged();
    }

    public void Dispose()
    {
        _dotNetObjectReference?.Dispose();
    }
}