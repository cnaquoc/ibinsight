using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBI.Core
{
    public static class JsonHelpers
    {
        public static String Json(this Object value)
        {
            if (value == null) return null;
            return JsonConvert.SerializeObject(value);
        }

        public static T FromJson<T>(this String json)
        {
            if (json == null) return default(T);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
