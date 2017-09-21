using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Data.Helpers
{
    public static class GuidHelpers
    {
        public static bool IsEmpty(this Guid? id)
        {
            return id == null || id.Value == Guid.Empty;
        }

        public static bool IsEmpty(this Guid id)
        {
            return id == null || id == Guid.Empty;
        }

        public static bool IsEmptyGuid(this string idStr)
        {
            return idStr == null || idStr.IsEmpty() || new Guid(idStr) == Guid.Empty;
        }

        public static Guid GetSafe(this Guid? id)
        {
            return id.IsEmpty() ? Guid.Empty : id.Value;
        }

        public static Guid? GetNullableSafe(this Guid id)
        {
            return id.IsEmpty() ? null : (Guid?)id;
        }

        public static Guid? GetNullableSafe(this Guid? id)
        {
            return id.IsEmpty() ? null : id;
        }

        public static Guid? GetGuidSafe(this String id)
        {
            return id.IsEmptyGuid() ? null : (Guid?)new Guid(id);
        }

        public static String ToStringSafe(this Guid? id)
        {
            return id.ToStringSafe(null);
        }

        public static String ToStringSafe(this Guid? id, String defaultValue)
        {
            return id.IsEmpty() ? defaultValue : id.Value.ToString();
        }

        public static String ToStringSafe(this Guid id)
        {
            return id.ToStringSafe(null);
        }

        public static String ToStringSafe(this Guid id, String defaultValue)
        {
            return id.IsEmpty() ? null : id.ToString();
        }
    }
}
