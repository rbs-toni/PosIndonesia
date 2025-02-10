using Microsoft.JSInterop;
using PosIndonesia.Models;

namespace PosIndonesia.Services;
public class PostalInterop : JSInterop
{
    readonly AppVersion _version;

    public PostalInterop(IJSRuntime jSRuntime, AppVersion version) : base(jSRuntime, "PostalJsInterop.js")
    {
        _version = version;
    }

    public async ValueTask<PagedPostal> GetPagedPostalAsync(
        string? searchString,
        SortOrderCollection? sorts = null,
        int page = 1,
        int pageSize = -1)
    {
        return await InvokeAsync<PagedPostal>("filter", _version.Version, searchString, sorts, page, pageSize);
    }
    public async ValueTask<MiniStats> GetStatsAsync(CancellationToken cancellationToken = default)
    {
        return await InvokeAsync<MiniStats>("getStats", _version.Version);
    }
}
