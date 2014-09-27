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
            var batch = new
            {
                value = new[]
                {
                    new
                    {
                        id = Guid.NewGuid().ToString(),
                        description = textbook.Description,
                        isbn = textbook.Isbn,
                        price = textbook.Price,
                        title = textbook.Title,
                        updatedDate = DateTime.UtcNow,
                        createdDate = DateTime.UtcNow,
                        campusName = textbook.CampusName
                    }
                }
            };

            Uri uri = new Uri(_serviceUri, "/indexes/textbook/docs/index");
            string json = AzureSearchHelper.SerializeJson(batch);
            HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Post, uri, json);
            response.EnsureSuccessStatusCode();
        }

        public IQueryable<Textbook> All(TextbookSearchOption searchOptionOption)
        {
            var results = Search(searchOptionOption.Keyword, searchOptionOption.CampusName, null, null, null, null, null);
            

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

        public dynamic Search(string searchText, string campusName, string sort, string title, string description, double? priceFrom, double? priceTo)
        {
            string search = "&search=" + Uri.EscapeDataString(searchText);
            string facets = "&facet=title&facet=description&facet=isbn&facet=price,values:10|25|100|500|1000|2500";
            string paging = "&$top=10";
            string filter = BuildFilter(campusName, title, description, priceFrom, priceTo);
            string orderby = BuildSort(sort);

            Uri uri = new Uri(_serviceUri, "/indexes/textbook/docs?$count=true" + search + facets + paging + filter + orderby);
            HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Get, uri);
            AzureSearchHelper.EnsureSuccessfulSearchResponse(response);

            return AzureSearchHelper.DeserializeJson<dynamic>(response.Content.ReadAsStringAsync().Result);
        }

        private string BuildFilter(string campusName, string title, string description, double? priceFrom, double? priceTo)
        {
            // carefully escape and combine input for filters, injection attacks that are typical in SQL
            // also apply here. No "DROP TABLE" risk, but a well injected "or" can cause unwanted disclosure

            string filter = String.Format("&$filter=campusName eq '{0}'", campusName);

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