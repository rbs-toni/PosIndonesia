using System.Collections;

namespace PosIndonesia.Models;
public record PostalCollection : IEnumerable<Postal>
{
    public Postal this[int index] => _postals[index];
    readonly List<Postal> _postals = [];
    public void Add(Postal postal) => _postals.Add(postal);
    public void Remove(Postal postal) => _postals.Remove(postal);
    public IEnumerator<Postal> GetEnumerator() => _postals.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
