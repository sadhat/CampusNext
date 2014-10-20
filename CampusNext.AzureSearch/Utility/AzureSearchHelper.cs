using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CampusNext.AzureSearch.Utility
{
    public class AzureSearchHelper
    {
        public const string ApiVersionString = "api-version=2014-07-31-Preview";

        private static readonly JsonSerializerSettings JsonSettings;

        static AzureSearchHelper()
        {
            JsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented, // for readability, change to None for compactness
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            JsonSettings.Converters.Add(new StringEnumConverter());
        }

        public static string SerializeJson(object value)
        {
            return JsonConvert.SerializeObject(value, JsonSettings);
        }

        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JsonSettings);
        }

        public static async Task<HttpResponseMessage> SendSearchRequest(HttpClient client, HttpMethod method, Uri uri, string json = null)
        {
            UriBuilder builder = new UriBuilder(uri);
            string separator = string.IsNullOrWhiteSpace(builder.Query) ? string.Empty : "&";
            builder.Query = builder.Query.TrimStart('?') + separator + ApiVersionString;

            var request = new HttpRequestMessage(method, builder.Uri);

            if (json != null)
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return await client.SendAsync(request);
        }

        public static void EnsureSuccessfulSearchResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string error = response.Content == null ? null : response.Content.ReadAsStringAsync().Result;
                throw new Exception("Search request failed: " + error);
            }
        }
    }
}
