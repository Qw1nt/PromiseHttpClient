using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace HttpClient.Core
{
    public static class HttpConfiguration
    {
        private static HttpConfigurationData _data;

        static HttpConfiguration()
        {
        }

        public static string Domain
        {
            get
            {
                if (_data == null)
                    _data = Resources.Load<HttpConfigurationData>(Config.ConfigDataPath);

                return _data.Domain;
            }
        }
    }

    public static class HttpTools
    {
        private static JsonSerializerSettings _settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static Uri BuildUri(string endpoint)
        {
            return new Uri($"{HttpConfiguration.Domain}{endpoint}");
        }

        public static byte[] ToJsonBody(this object payload)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload, Formatting.Indented, _settings));
        }

        public static void DebugAsJson(this object target)
        {
            Debug.Log(JsonConvert.SerializeObject(target, Formatting.Indented, _settings));
        }
    }
}