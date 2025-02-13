namespace PosIndonesia.Models;
public record Postal
{
    public int Code { get; set; }
    public string? Village { get; set; }
    public string? District { get; set; }
    public string? Regency { get; set; }
    public string? Province { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public int Elevation { get; set; }
    public string? Timezone { get; set; }
    public string UniqueKey()
    {
        return Guid.NewGuid().ToString("N");
    }
}
