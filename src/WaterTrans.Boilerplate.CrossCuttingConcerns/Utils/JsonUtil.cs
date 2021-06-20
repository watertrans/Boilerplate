using System.Collections.Generic;
using System.Dynamic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.Utils
{
    public static class JsonUtil
    {
        public static JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(),
            },
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        };

        public static string Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }

            return JsonSerializer.Serialize(value, JsonSerializerOptions);
        }

        public static T Deserialize<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(value, JsonSerializerOptions);
        }

        public static string ToRawJsonArray(string json, string key)
        {
            if (string.IsNullOrEmpty(json))
            {
                return "[]";
            }

            var result = new List<object>();
            var objects = Deserialize<List<ExpandoObject>>(json);

            foreach (IDictionary<string, object> obj in objects)
            {
                if (obj.ContainsKey(key))
                {
                    result.Add(obj[key]);
                }
            }

            return Serialize(result);
        }
    }
}
