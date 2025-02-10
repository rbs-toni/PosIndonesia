namespace PosIndonesia.Models;
public class SortOrder
{
    public SortOrder()
    {
        Name = "";
    }
    public SortOrder(string name, string? dir)
    {
        Name = name;
        Dir = dir;
    }
    public string Name { get; set; }
    public string? Dir { get; set; }
}
