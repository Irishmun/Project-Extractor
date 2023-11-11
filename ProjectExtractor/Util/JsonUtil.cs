using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ProjectExtractor.Util
{
    public static class JsonUtil
    {
        public static string ToJson<T>(T value)
        {
            return JsonSerializer.Serialize(value);
        }

        public static T FromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
