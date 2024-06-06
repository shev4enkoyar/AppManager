using Microsoft.AspNetCore.Components;
using MudExtensions.Enums;

namespace WebUIBlazor.Components.Shared;

public partial class CustomPaperCard : ComponentBase
{
    [Parameter]
    public Guid AnimationId { get; set; } = Guid.NewGuid();

    [Parameter]
    public string Class { get; set; } = "pa-4";

    [Parameter]
    public string Style { get; set; } = "height: 200px;";

    [Parameter]
    public int Elevation { get; set; } = 2;
    
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;
    
    [Parameter]
    public EventCallback OnClick { get; set; }
    
    double _duration = 2;
    AnimationTiming _animationTiming = AnimationTiming.EaseInOut;
    AnimationDirection _animationDirection = AnimationDirection.Alternate;
    AnimationFillMode _animationFillMode = AnimationFillMode.None;
    double _value = 30;
    
    private async Task OnClickHandler()
    {
        await OnClick.InvokeAsync();
    }
}

