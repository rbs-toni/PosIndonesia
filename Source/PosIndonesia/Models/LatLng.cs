using System;
using System.Linq;

namespace PosIndonesia.Models;

public record LatLng
{
    public LatLng(float lat, float lngi)
    {
        Lat = lat;
        Lng = lngi;
    }
    public float Lat { get; set; }
    public float Lng { get; set; }
}
