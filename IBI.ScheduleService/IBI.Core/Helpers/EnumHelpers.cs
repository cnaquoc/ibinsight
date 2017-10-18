using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBI.Core
{
    public static class EnumHelpers
    {
        public static List<DataItem<T>> EnumToList<T>()
        {
            return EnumToList<T>(null);
        }

        public static List<DataItem<T>> EnumToList<T>(Func<T, String> toLanguageFunc)
        {
            List<DataItem<T>> lst = new List<DataItem<T>>();

            var values = Enum.GetValues(typeof(T)).Cast<T>();
            foreach (T val in values)
            {
                lst.Add(new DataItem<T>(val, (toLanguageFunc == null) ? val.ToString() : toLanguageFunc(val)));
            }

            return lst;
        }
    }
}
