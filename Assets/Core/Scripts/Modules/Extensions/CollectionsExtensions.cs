using System;
using System.Collections.Generic;

public static class CollectionsExtensions
{
    private static readonly Random Random = new ();

    public static T GetRandomElement<T>(this IReadOnlyList<T> list)
    {
        if (list == null || list.Count == 0)
            throw new ArgumentException("Collection cannot be null or empty.");

        var index = Random.Next(list.Count);
        return list[index];
    }
}