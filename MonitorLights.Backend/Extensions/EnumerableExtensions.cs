namespace MonitorLights.Backend.Extensions;

public static class EnumerableExtensions
{
    public static int IndexOf<T>(
        this IEnumerable<T> source,
        T value,
        IEqualityComparer<T> comparer = null
    )
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        comparer ??= EqualityComparer<T>.Default;

        int index = 0;
        foreach (var item in source)
        {
            if (comparer.Equals(item, value))
                return index;
            index++;
        }
        return -1;
    }

    public static int FindIndex<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate
    )
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        int index = 0;
        foreach (var item in source)
        {
            if (predicate(item))
                return index;
            index++;
        }
        return -1;
    }
}