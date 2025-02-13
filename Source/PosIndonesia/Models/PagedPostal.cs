namespace PosIndonesia.Models;
public record PagedPostal
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public List<Postal>? Items { get; set; }
    public bool HasData => Items?.Count > 0;
}
