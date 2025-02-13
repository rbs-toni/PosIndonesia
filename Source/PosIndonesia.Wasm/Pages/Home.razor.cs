using PosIndonesia.Models;

namespace PosIndonesia.Wasm.Pages;
public partial class Home
{
    LatLng? _latLng;
    MiniStats? _miniStats;
    SortOrderCollection _orders = [];
    PagedPostal? _pagedPostal;
    string? _searchString;

    protected override async Task OnInitializedAsync()
    {
        _miniStats = await Postal.GetStatsAsync();
        _pagedPostal = await Postal.GetPagedPostalAsync(_searchString, null, 1, 100);
        StateHasChanged();
    }

    void OnPostalSelected(Postal postal)
    {
        _latLng = new LatLng(postal.Latitude, postal.Longitude);
    }
    async Task OnSearchStringChanged()
    {
        _pagedPostal = await Postal.GetPagedPostalAsync(
            _searchString,
            _orders,
            1,
            _pagedPostal?.PageSize ?? 100);
    }

    async Task OnSortOrderChanged(SortOrderCollection sortOrders)
    {
        _orders = sortOrders;
        _pagedPostal = await Postal.GetPagedPostalAsync(
            _searchString,
            _orders,
             1,
            _pagedPostal?.PageSize ?? 100);
    }
    async Task OnPageChanged(int page)
    {
        _pagedPostal = await Postal.GetPagedPostalAsync(
            _searchString,
            _orders,
            page,
            _pagedPostal?.PageSize ?? 100);
    }
    async Task OnPageSizeChanged(int pageSize)
    {
        _pagedPostal = await Postal.GetPagedPostalAsync(
            _searchString,
            _orders,
             _pagedPostal?.Page ?? 1,
            pageSize);
    }

    string OrderToDirection(int order)
    {
        if (order == 1)
        {
            return "asc";
        }
        else if (order == 2)
        {
            return "desc";
        }
        else
        {
            return string.Empty;
        }
    }
}
