using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBI.Data.Helpers
{
    public static class EnumerableHelpers
    {
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Count() == 0;
        }
    }
}
