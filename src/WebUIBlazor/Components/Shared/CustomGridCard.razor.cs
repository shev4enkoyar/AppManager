using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebUIBlazor.Components.Shared;

public partial class CustomGridCard : ComponentBase
{
    private bool IsShowCardButtons { get; set; } = false;

    [Parameter]
    public string Title { get; set; } = string.Empty;
    
    [Parameter]
    public string Text { get; set; } = string.Empty;
    
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseEnterAfter { get; set; }
    
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseEnterBefore { get; set; }
    
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseLeaveAfter { get; set; }
    
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseLeaveBefore { get; set; }
    
    [Parameter]
    public EventCallback<MouseEventArgs> OnCardClicked { get; set; }
    
    [Parameter]
    public EventCallback<MouseEventArgs> OnSettingButtonClick { get; set; }
    
    private async Task OnCardMouseEnterHandler()
    {
        await OnMouseEnterBefore.InvokeAsync();
        IsShowCardButtons = true;
        await OnMouseEnterAfter.InvokeAsync();
    }
    
    private async Task OnCardMouseLeaveHandler()
    {
        await OnMouseLeaveBefore.InvokeAsync();
        IsShowCardButtons = false;
        await OnMouseLeaveAfter.InvokeAsync();
    }

    private async Task OnCardClickHandler(MouseEventArgs args)
    {
        await OnCardClicked.InvokeAsync();
    }
    
    private async Task OnSettingButtonClickHandler(MouseEventArgs obj)
    {
        await OnSettingButtonClick.InvokeAsync();
    }
}

