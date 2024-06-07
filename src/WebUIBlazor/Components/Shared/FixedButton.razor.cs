using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebUIBlazor.Components.Shared;

public partial class FixedButton : ComponentBase
{
    [Parameter] public EventCallback OnClick { get; set; }

    [Parameter] public string Icon { get; set; } = Icons.Material.Filled.ArrowBack;

    private async Task OnClickHandler()
    {
        await OnClick.InvokeAsync();
    }
}
