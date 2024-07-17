using System.Collections.Generic;

public class Pinia
{
    static readonly Dictionary<PiniaItem, object> data = new();

    public static T Get<T>(PiniaItem item)
    {
        return (T)data[item];
    }

    public static void Set<T>(PiniaItem item, T data)
    {
        Pinia.data[item] = data;
    }
}

public enum PiniaItem
{
    DEATH_REASON
}
