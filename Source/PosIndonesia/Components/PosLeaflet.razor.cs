using PosIndonesia.Models;
using PosIndonesia.Utilities;

namespace PosIndonesia;
public partial class PosLeaflet : PosJSComponentBase
{
    public PosLeaflet() : base("PosLeaflet")
    {
        Id = IDGenerator.NewId();
    }
    public override async Task OnAfterImportAsync()
    {
        var geo = await GetGeolocationAsync();
        var latlng = new LatLng(float.Parse(geo.Latitude ?? string.Empty), float.Parse(geo.Longitude ?? string.Empty));
        await InitMapAsync(latlng, 15);
    }
    async Task<Geolocation> GetGeolocationAsync()
    {
        return await InvokeAsync<Geolocation>("getGeo");
    }
    async Task InitMapAsync(LatLng latLng, int zoom)
    {
        await InvokeVoidAsync("leaflet.init", Id, latLng, zoom);
    }

    public async Task SetViewAsync(LatLng latLng)
    {
        await InvokeVoidAsync("leaflet.setView", Id, latLng);
    }

    public async Task FlyToAsync(LatLng latLng)
    {
        await InvokeVoidAsync("leaflet.flyTo", Id, latLng);
    }
}
