using System;
using System.Collections.Generic;
using System.Text;

namespace IBI.Data.Helpers
{
    public static class StringHelpers
    {
        public static bool IsEmpty(this String s)
        {
            return String.IsNullOrWhiteSpace(s);
        }

        public static int Year(this int? partialDate)
        {
            if (String.IsNullOrEmpty(partialDate.Value.ToString())) return 0;
            return partialDate.Value / 10000;

        }

        public static int Year(this int partialDate)
        {
            return Year((int?)partialDate);
        }

        public static int Month(this int? partialDate)
        {
            if (String.IsNullOrEmpty(partialDate.Value.ToString())) return 0;
            return (partialDate.Value % 10000) / 100;

        }

        public static int Month(this int partialDate)
        {
            return Month((int?)partialDate);
        }

        public static int Day(this int? partialDate)
        {
            if (String.IsNullOrEmpty(partialDate.Value.ToString())) return 0;
            return partialDate.Value % 100;
        }

        public static int Day(this int partialDate)
        {
            return Day((int?)partialDate);
        }
    }
}
