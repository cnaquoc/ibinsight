using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBI.Data.Helpers
{
    static class JsonHelpers
    {
        public static String ToJson(this Object value)
        {
            if (value == null) return null;
            return JsonConvert.SerializeObject(value);
        }

        public static T ToObject<T>(this String json)
        {
            if (json == null) return default(T);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
