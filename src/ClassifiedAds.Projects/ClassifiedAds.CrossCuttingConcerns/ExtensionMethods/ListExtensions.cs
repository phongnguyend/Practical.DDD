using System.Collections.Generic;

namespace ClassifiedAds.CrossCuttingConcerns.ExtensionMethods
{
    public static class ListExtensions
    {
        public static List<T> Combines<T>(this List<T> collection1, List<T> collection2)
        {
            collection1.AddRange(collection2);
            return collection1;
        }

        public static ICollection<T> Combines<T>(this ICollection<T> collection1, ICollection<T> collection2)
        {
            var list = new List<T>();
            list.AddRange(collection1);
            list.AddRange(collection2);
            return list;
        }
    }
}
