using System.Collections;

namespace PosIndonesia.Models;
public class SortOrderCollection : IEnumerable<SortOrder>
{
    private readonly List<SortOrder> _sortOrders = new();

    public void Add(string name, string direction) => _sortOrders.Add(new SortOrder(name, direction));
    public void Add(SortOrder order) => _sortOrders.Add(order);
    public void Remove(SortOrder order) => _sortOrders.Remove(order);

    public int IndexOf(string name)
    {
        var order = _sortOrders.FirstOrDefault(o => o.Name == name);
        if (order != null)
        {
            return _sortOrders.IndexOf(order);
        }
        return -1;
    }

    public IEnumerator<SortOrder> GetEnumerator() => _sortOrders.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
