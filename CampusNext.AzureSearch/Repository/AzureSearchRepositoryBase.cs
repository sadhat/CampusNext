using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using CampusNext.AzureSearch.Utility;
using CampusNext.Entity;
using RedDog.Search;
using RedDog.Search.Http;
using RedDog.Search.Model;
using Index = AzureSearchClient.Index;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CampusNext.AzureSearch.Repository
{
    public abstract class AzureSearchRepositoryBase : IAzureSearchRepository
    {
        private static Uri _serviceUri;
        private static HttpClient _httpClient;
        private readonly string _indexName;
        protected readonly string ServiceName;
        protected readonly string ServiceApiKey;
       
        protected AzureSearchRepositoryBase(string serviceName, string serviceApiKey, string indexName)
        {
            _indexName = indexName;
            ServiceName = serviceName;
            ServiceApiKey = serviceApiKey;
            _serviceUri = new Uri("https://" + serviceName + ".search.windows.net");
            _httpClient = new HttpClient();
            // Get the search service connection information from the App.config
            _httpClient.DefaultRequestHeaders.Add("api-key", serviceApiKey);
            _serviceUri = new Uri(_serviceUri, "/indexes");
        }

        public async Task<HttpResponseMessage> AddAsync(IEntity entity)
        {
            Uri uri = new Uri(_serviceUri, "/indexes/" + _indexName + "/docs/index");
            string json = AzureSearchHelper.SerializeJson(GetEntityDefinition(entity));
            HttpResponseMessage response = await AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Post, uri, json);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> DeleteAsync(IEntity entity)
        {
            Uri uri = new Uri(_serviceUri, "/indexes/" + _indexName + "/docs/index");
            string json = AzureSearchHelper.SerializeJson(TransformEntityForDelete(entity)).Replace("search_action", "@search.action");
            HttpResponseMessage response = await AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Post, uri, json);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> UpdateAsync(IEntity entity)
        {
            Uri uri = new Uri(_serviceUri, "/indexes/" + _indexName + "/docs/index");
            string json = AzureSearchHelper.SerializeJson(GetEntityDefinition(entity));
            HttpResponseMessage response = await AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Post, uri, json);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<int> Count()
        {
            Uri uri = new Uri(_serviceUri, "/indexes/" + _indexName + "/docs/$count");
            HttpResponseMessage response = await AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Get, uri);
            int count = AzureSearchHelper.DeserializeJson<int>(response.Content.ReadAsStringAsync().Result);
            return count;
        }

        public async Task<int> Count(string indexName, string campusCode)
        {
            var connection = ApiConnection.Create(ServiceName, ServiceApiKey);
            var queryClient = new IndexQueryClient(connection);
            var query = new SearchQuery();
            query.Filter = String.Format("campusCode eq '{0}'", campusCode);

            var result = await queryClient.SearchAsync(indexName, query.Count(true));
            return result.Body.Count;
        }

        public RedDog.Search.Model.Index GetIndex(string indexName)
        {
            var connection = ApiConnection.Create(ServiceName, ServiceApiKey);
            var managementClient = new IndexManagementClient(connection);
            return managementClient.GetIndexAsync(indexName).Result.Body;
        }

        public async Task<T> Get<T>(string key)
        {
            Uri uri = new Uri(_serviceUri, "/indexes/" + _indexName + "/docs/" + key);
            HttpResponseMessage response = await AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Get, uri);
            string responseContent = response.Content.ReadAsStringAsync().Result;
            var document = AzureSearchHelper.DeserializeJson<T>(responseContent);
            return document;
        }

        public abstract Task<IList<IEntity>> Search(string keyword, string campus = null,
            IDictionary<string, string> filterDictionary = null);

        public abstract dynamic TransformEntity(IEntity entity);

        private dynamic GetEntityDefinition(IEntity entity)
        {
            return new
            {
                value = new[]
                {
                    TransformEntity(entity)
                }
            };
        }

        private dynamic TransformEntityForDelete(IEntity entity)
        {
            return new
            {
                value = new[]
                {
                    new
                    {
                        search_action = "delete",
                        id = entity.Id.ToString()
                    }
                }
            };
        }
    }
}
