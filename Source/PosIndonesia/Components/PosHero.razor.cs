using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PosIndonesia.Utilities;

namespace PosIndonesia;
public partial class PosHero
{
    public PosHero()
    {
        Id = IDGenerator.NewId();
    }

    [Inject]
    IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/PosIndonesia/Components/PosHero.razor.js");
            if (module != null)
            {
                await module.InvokeVoidAsync("init", Id);
            }
        }
    }
}
