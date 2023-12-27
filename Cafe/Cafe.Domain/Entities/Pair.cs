namespace Cafe.Domain.Entities;

public struct Pair<T, V>
{
    public T First { get; set; }
    public V Second { get; set; }

    public Pair(T first, V second) => 
        (First, Second) = (first, second);
}
