using System;
using System.Collections.Generic;
using System.Linq;

namespace Obscuro.Utils
{
    public static class CollectionExtensions
    {
        public static int Xor<T>(this IEnumerable<T> items)
            => items.Xor(x => x.GetHashCode());

        public static int Xor<T>(this IEnumerable<T> items, Func<T, int> hashcodeGetter)
            => items.Aggregate(0, (res, x) => res ^= hashcodeGetter(x));

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
            => items.ForEach(collection.Add);

        public static void AddRange<T>(this ISet<T> collection, IEnumerable<T> items)
            => items.ForEach(collection.Add);

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        public static void ForEach<T, TResult>(this IEnumerable<T> items, Func<T, TResult> action)
        {
            foreach (var item in items)
                action(item);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
            => items == null || !items.Any();

        public static T GetItemAtOrDefault<T>(this IReadOnlyList<T> items, int index, T defaultValue = default(T))
        {
            if (items.IsNullOrEmpty() || items.Count <= index)
                return defaultValue;

            return items[index];
        }
    }
}
