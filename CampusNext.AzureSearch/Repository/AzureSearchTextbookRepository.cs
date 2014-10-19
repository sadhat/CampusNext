﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using CampusNext.Entity;
using RedDog.Search;
using RedDog.Search.Http;
using RedDog.Search.Model;

namespace CampusNext.AzureSearch.Repository
{
    public class AzureSearchTextbookRepository: AzureSearchRepositoryBase
    {
        public AzureSearchTextbookRepository(string serviceName, string serviceApiKey)
            : base(serviceName, serviceApiKey, "textbook") {}

        public AzureSearchTextbookRepository()
            : this(ConfigurationManager.AppSettings["SearchServiceName"], ConfigurationManager.AppSettings["SearchServiceApiKey"])
        {
            
        }

        public override dynamic TransformEntity(IEntity entity)
        {
            var textbook = (Textbook) entity;
            return new
            {
                id = textbook.Id.ToString(CultureInfo.InvariantCulture),
                description = textbook.Description,
                isbn = textbook.Isbn,
                price = textbook.Price,
                name = textbook.Name,
                updatedDate = DateTime.UtcNow,
                createdDate = DateTime.UtcNow,
                campusCode = textbook.CampusCode,
                authors = textbook.Authors,
                course = textbook.Course
            };
        }

        public override async Task<IList<IEntity>> Search(string keyword, string campus = null, IDictionary<string, string> filterDictionary = null)
        {
            var connection = ApiConnection.Create(ServiceName, ServiceApiKey);
            var queryClient = new IndexQueryClient(connection);
            var query = new SearchQuery(keyword + "*");
            
            if(!String.IsNullOrWhiteSpace(campus))
                query.Filter = String.Format("campusCode eq '{0}'", campus);

            var result = await queryClient.SearchAsync("textbook", query);
            IList<IEntity> list = new List<IEntity>();
            foreach (var record in result.Body.Records)
            {
                var textbook = new Textbook
                {
                    Id = int.Parse(record.Properties["id"].ToString()),
                    Description = record.Properties["description"].ToString(),
                    Name = record.Properties["name"].ToString(),
                    CampusCode =
                        record.Properties["campusCode"] == null
                            ? string.Empty
                            : record.Properties["campusCode"].ToString()
                };
                list.Add(textbook);
            }
            return list;
        }
    }
}
