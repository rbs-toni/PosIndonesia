namespace PosIndonesia.Models;
public record MiniStats
{
    public int ProvincesCount { get; set; }
    public int RegenciesCount { get; set; }
    public int DistrictsCount { get; set; }
    public int VillagesCount { get; set; }
}
