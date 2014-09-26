using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace CampusNext.Services
{
    public class AzureSearchSeeder
    {
        private static Uri _serviceUri;
        private static HttpClient _httpClient;

        public static void Seed()
        {
            _serviceUri = new Uri("https://" + ConfigurationManager.AppSettings["SearchServiceName"] + ".search.windows.net");
            _httpClient = new HttpClient();
            // Get the search service connection information from the App.config
            _httpClient.DefaultRequestHeaders.Add("api-key", ConfigurationManager.AppSettings["SearchServiceApiKey"]);

            DeleteCatalogIndex();
            CreateCatalogIndex();

            DeleteSampleData();
            AddSampleData();
        }   

        private static void CreateCatalogIndex()
        {
            var definition = new
            {
                Name = "textbook",
                Fields = new[] 
                { 
                    new { Name = "id",               Type = "Edm.String",         Key = true,  Searchable = false, Filterable = false, Sortable = false, Facetable = false, Retrievable = true,  Suggestions = false },
                    new { Name = "title",            Type = "Edm.String",         Key = false, Searchable = true,  Filterable = false, Sortable = true,  Facetable = true, Retrievable = true,  Suggestions = true  },
                    new { Name = "description",      Type = "Edm.String",         Key = false, Searchable = true,  Filterable = false, Sortable = false, Facetable = true, Retrievable = true,  Suggestions = true  },
                    new { Name = "isbn",             Type = "Edm.String",         Key = false, Searchable = true,  Filterable = true,  Sortable = true,  Facetable = true,  Retrievable = true,  Suggestions = false },
                    new { Name = "price",            Type = "Edm.Double",         Key = false, Searchable = false, Filterable = false, Sortable = false, Facetable = true, Retrievable = true,  Suggestions = false },
                    new { Name = "updatedDate",      Type = "Edm.DateTimeOffset", Key = false, Searchable = false, Filterable = true,  Sortable = false, Facetable = false, Retrievable = false, Suggestions = false },
                    new { Name = "createdDate",      Type = "Edm.DateTimeOffset", Key = false, Searchable = false, Filterable = true,  Sortable = false, Facetable = false, Retrievable = false, Suggestions = false },
                    new { Name = "campusName",       Type = "Edm.String",         Key = false, Searchable = true,  Filterable = true,  Sortable = false, Facetable = true,  Retrievable = true,  Suggestions = true  },
                   }
            };

            Uri uri = new Uri(_serviceUri, "/indexes");
            string json = AzureSearchHelper.SerializeJson(definition);
            HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Post, uri, json);
            response.EnsureSuccessStatusCode();
        }

        private static void AddSampleData()
        {
            var batch = new
            {
                value = new[]
                {
                    new
                    {

                        id = Guid.NewGuid().ToString(),
                        description =
                            "When you have a question about C# 5.0 or the .NET CLR, this bestselling guide has precisely the answers you need. Uniquely organized around concepts and use cases, this updated fifth edition features a reorganized section on concurrency, threading, and parallel programming—including in-depth coverage of C# 5.0’s new asynchronous functions.",
                        isbn = "978-1449320102",
                        price = 34.33,
                        title = "C# 5.0 in a Nutshell: The Definitive Reference",
                        updatedDate = DateTime.UtcNow,
                        createdDate = DateTime.UtcNow,
                        campusName = "NDSU"
                    },
                    new
                    {
                        id = Guid.NewGuid().ToString(),
                        description =
                            "This new edition of this invaluable reference brings the coverage of software metrics fully up to date. The book has been rewritten and redesigned to take into account the fast changing developments in software metrics, most notably their widespread penetration into industrial practice. New sections deal with prcess maturity and measurement, goal-question-metric, metrics plans, experimentation, empirical studies, object-oriented metrics, and metrics tools. 88 line illustrations.",
                        isbn = "978-0534956004",
                        price = 44.99,
                        title = "Software Metrics",
                        updatedDate = DateTime.UtcNow,
                        createdDate = DateTime.UtcNow,
                        campusName = "NDSU"
                    }
                }
            };

            Uri uri = new Uri(_serviceUri, "/indexes/textbook/docs/index");
            string json = AzureSearchHelper.SerializeJson(batch);
            HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Post, uri, json);
            response.EnsureSuccessStatusCode();

            //throw new NotImplementedException();
        }

        private static bool DeleteCatalogIndex()
        {
            Uri uri = new Uri(_serviceUri, "/indexes/textbook");
            HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Delete, uri);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            return true;
        }
        
        private static void DeleteSampleData()
        {
            //throw new NotImplementedException();
        }

        

    }
}