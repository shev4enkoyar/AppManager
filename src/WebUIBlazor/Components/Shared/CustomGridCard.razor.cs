using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudExtensions.Enums;

namespace WebUIBlazor.Components.Shared;

public partial class CustomGridCard : ComponentBase
{
    private AnimationDirection _animationDirection = AnimationDirection.Alternate;
    private AnimationFillMode _animationFillMode = AnimationFillMode.None;
    private AnimationTiming _animationTiming = AnimationTiming.EaseInOut;
    private double _duration = 2;
    private double _value = 30;
    private bool IsShowCardButtons { get; set; } = false;

    [Parameter] public string Title { get; set; } = string.Empty;

    [Parameter] public string Text { get; set; } = string.Empty;

    [Parameter] public EventCallback<MouseEventArgs> OnMouseEnterAfter { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnMouseEnterBefore { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnMouseLeaveAfter { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnMouseLeaveBefore { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnCardClicked { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnSettingButtonClick { get; set; }

    [Parameter] public Guid AnimationId { get; set; } = Guid.NewGuid();

    [Parameter] public int Xs { get; set; } = 12;
    [Parameter] public int Sm { get; set; } = 6;
    [Parameter] public int Md { get; set; } = 3;

    [Parameter] public Typo HeaderTypo { get; set; } = Typo.h6;

    [Parameter] public bool ShowSettingButtonAlways { get; set; } = false;

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
