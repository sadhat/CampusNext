using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using CampusNext.Services.Entity;
using CampusNext.Services.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


namespace CampusNext.Services.BusinessLayer.AzureSearch
{
    public class TextbookRepository : ITextbookRepository
    {
        private static Uri _serviceUri;
        private static HttpClient _httpClient;

        public TextbookRepository()
        {
            _serviceUri = new Uri("https://" + ConfigurationManager.AppSettings["SearchServiceName"] + ".search.windows.net");
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("api-key", ConfigurationManager.AppSettings["SearchServiceApiKey"]);
        }
        public void Add(Textbook textbook)
        {
            // Retrieve the storage account from the connection string.
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            var table = tableClient.GetTableReference("Textbook");

            var newTextbook = new TextbookEntity(textbook.CampusName, Guid.NewGuid())
            {
                Description = textbook.Description,
                Isbn = textbook.Isbn,
                Price = textbook.Price,
                Title = textbook.Title,
                CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow
            };

            var insertOperation = TableOperation.Insert(newTextbook);
            table.Execute(insertOperation);
        }

        public IQueryable<Textbook> All(TextbookSearchOption searchOptionOption)
        {
            var results = Search("0534956004", null, null, null, null, null);
            

            var items = new List<Textbook>();

            foreach (var result in results.value)
            {
                var id = result.id;
                var title = result.title;
                var description = result.description;
                var price = result.price;
                var isbn = result.isbn;
                items.Add(new Textbook
                {
                    Id = id,
                    Title = title,
                    Description =  description,
                    Price = price,
                    Isbn = isbn
                });
                

            }


            return items.AsQueryable();
        }

        public dynamic Search(string searchText, string sort, string title, string description, double? priceFrom, double? priceTo)
        {
            string search = "&search=" + Uri.EscapeDataString(searchText);
            string facets = "&facet=title&facet=description&facet=isbn&facet=price,values:10|25|100|500|1000|2500";
            string paging = "&$top=10";
            string filter = BuildFilter(title, description, priceFrom, priceTo);
            string orderby = BuildSort(sort);

            Uri uri = new Uri(_serviceUri, "/indexes/textbook/docs?$count=true" + search + facets + paging + filter + orderby);
            HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Get, uri);
            AzureSearchHelper.EnsureSuccessfulSearchResponse(response);

            return AzureSearchHelper.DeserializeJson<dynamic>(response.Content.ReadAsStringAsync().Result);
        }

        private string BuildFilter(string title, string description, double? priceFrom, double? priceTo)
        {
            // carefully escape and combine input for filters, injection attacks that are typical in SQL
            // also apply here. No "DROP TABLE" risk, but a well injected "or" can cause unwanted disclosure

            string filter = "&$filter=campusName eq 'NDSU'";

            if (!string.IsNullOrWhiteSpace(title))
            {
                filter += " and title eq '" + EscapeODataString(title) + "'";
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                filter += " and description eq '" + EscapeODataString(description) + "'";
            }

            if (priceFrom.HasValue)
            {
                filter += " and price ge " + priceFrom.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (priceTo.HasValue && priceTo > 0)
            {
                filter += " and price le " + priceTo.Value.ToString(CultureInfo.InvariantCulture);
            }

            return filter;
        }
        private string BuildSort(string sort)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return string.Empty;
            }

            // could also add asc/desc if we want to allow both sorting directions
            if (sort == "price" || sort == "title")
            {
                return "&$orderby=" + sort;
            }

            throw new Exception("Invalid sort order");
        }

        private string EscapeODataString(string s)
        {
            return Uri.EscapeDataString(s).Replace("\'", "\'\'");
        }
    }
}