using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CampusNext.AzureSearch.Utility;

namespace CampusNext.AzureSearch.Indexer
{
    public abstract class IndexerBase : IIndexer
    {
        private static Uri _serviceUri;
        private static HttpClient _httpClient;
        private string _indexName;
        protected IndexerBase(string serviceName, string serviceApiKey, string indexName)
        {
            _indexName = indexName;
            _serviceUri = new Uri("https://" + serviceName + ".search.windows.net");
            _httpClient = new HttpClient();
            // Get the search service connection information from the App.config
            _httpClient.DefaultRequestHeaders.Add("api-key", serviceApiKey);
            _serviceUri = new Uri(_serviceUri, "/indexes");
        }

        public virtual void Create()
        {
            Uri uri = new Uri(_serviceUri, "/indexes");
            string json = AzureSearchHelper.SerializeJson(GetIndexDefinition());
            HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Post, uri, json).Result;
            response.EnsureSuccessStatusCode();
        }

        public virtual void Update()
        {
            Uri uri = new Uri(_serviceUri, "/indexes/" + _indexName);
            string json = AzureSearchHelper.SerializeJson(GetIndexDefinition());
            HttpResponseMessage response =
                AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Put, uri, json).Result;
            response.EnsureSuccessStatusCode();
        }

        public virtual bool Delete()
        {
            Uri uri = new Uri(_serviceUri, "/indexes/" + _indexName);
            HttpResponseMessage response =
                AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Delete, uri).Result;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            return true;
        }

        private dynamic GetIndexDefinition()
        {
            return new
            {
                Name = _indexName,
                Fields = GetFieldDefinition().ToArray()
            };
        }
        protected abstract List<dynamic> GetFieldDefinition();
    }
}
