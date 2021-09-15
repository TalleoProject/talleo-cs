sealed class VariantHolder<T> : IVariantHolder
{
    public T Item { get; }

    public bool Is<U>() => typeof(U) == typeof(T);

    public object Get() => Item;

    public VariantHolder(T item) => Item = item;
}

public sealed class Variant<T1, T2>
{
    private IVariantHolder variant;

    public bool Is<T>() => variant.Is<T>();

    public T Get<T>() => ((VariantHolder<T>)variant).Item;

    public object Get() => variant.Get();

    // T1 constructor, casts
    public Variant(T1 item)
        => variant = new VariantHolder<T1>(item);

    public static implicit operator Variant<T1, T2>(T1 item)
        => new Variant<T1, T2>(item);

    public static explicit operator T1(Variant<T1, T2> item)
        => item.Get<T1>();

    // T2 constructor, casts
    public Variant(T2 item)
        => variant = new VariantHolder<T2>(item);

    public static implicit operator Variant<T1, T2>(T2 item)
        => new Variant<T1, T2>(item);

    public static explicit operator T2(Variant<T1, T2> item)
        => item.Get<T2>();

    /* ...
       Ignoring Equals() and GetHashCode() for now */
}
