using PosIndonesia.Models;
using PosIndonesia.Utilities;

namespace PosIndonesia;
public partial class PosGoogleMap : PosJSComponentBase
{
    public PosGoogleMap() : base("PosGoogleMap")
    {
        Id = IDGenerator.NewId();
    }
    public override async Task OnAfterImportAsync()
    {
        var geo = await GetGeolocationAsync();
        var latlng = new LatLng(float.Parse(geo.Latitude ?? string.Empty), float.Parse(geo.Longitude ?? string.Empty));
        await InitMapAsync(latlng, 4);
    }
    async Task<Geolocation> GetGeolocationAsync()
    {
        return await InvokeAsync<Geolocation>("getGeo");
    }
    async Task InitMapAsync(LatLng latLng, int zoom)
    {
        await InvokeVoidAsync("googleMap.init", Id, latLng, zoom);
    }

    public async Task SetViewAsync(LatLng latLng)
    {
        await InvokeVoidAsync("googleMap.setCenter", Id, latLng);
    }
}
