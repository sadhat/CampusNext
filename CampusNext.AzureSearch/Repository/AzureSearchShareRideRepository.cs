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
    public class AzureSearchShareRideRepository: AzureSearchRepositoryBase
    {
        private const string IndexName = "shareride";
        public AzureSearchShareRideRepository(string serviceName, string serviceApiKey)
            : base(serviceName, serviceApiKey, IndexName) {}

        public AzureSearchShareRideRepository()
            : this(ConfigurationManager.AppSettings["SearchServiceName"], ConfigurationManager.AppSettings["SearchServiceApiKey"])
        {
            
        }

        public override dynamic TransformEntity(IEntity entity)
        {
            var shareRide = (ShareRide) entity;
            return new
            {
                id = shareRide.Id.ToString(CultureInfo.InvariantCulture),
                startDateTime = shareRide.StartDateTime,
                returnDateTime = shareRide.ReturnDateTime,
                isRoundtrip = shareRide.IsRoundTrip,
                additionalInfo = shareRide.AdditionalInfo,
                updatedDate = DateTime.UtcNow,
                createdDate = DateTime.UtcNow,
                campusCode = shareRide.CampusCode,
                fromLocation = shareRide.FromLocation,
                toLocation = shareRide.ToLocation
            };
        }

        public override async Task<IList<IEntity>> Search(string keyword, string campus = null, IDictionary<string, string> filterDictionary = null)
        {
            var connection = ApiConnection.Create(ServiceName, ServiceApiKey);
            var queryClient = new IndexQueryClient(connection);
            var query = new SearchQuery(keyword + "*");
            
            if(!String.IsNullOrWhiteSpace(campus))
                query.Filter = String.Format("campusCode eq '{0}'", campus);

            var result = await queryClient.SearchAsync(IndexName, query);
            IList<IEntity> list = new List<IEntity>();
            foreach (var record in result.Body.Records)
            {
                var shareRide = new ShareRide
                {
                    Id = int.Parse(record.Properties["id"].ToString()),
                    StartDateTime = record.Properties["startDateTime"].ToString(),
                    ReturnDateTime =
                        record.Properties["returnDateTime"] == null
                            ? string.Empty
                            : record.Properties["returnDateTime"].ToString(),
                    CampusCode =
                        record.Properties["campusCode"] == null
                            ? string.Empty
                            : record.Properties["campusCode"].ToString(),
                    IsRoundTrip =
                        record.Properties["isRoundtrip"] != null &&
                        bool.Parse(record.Properties["isRoundtrip"].ToString()),
                    AdditionalInfo =
                        record.Properties["additionalInfo"] == null
                            ? string.Empty
                            : record.Properties["additionalInfo"].ToString()
                };
                list.Add(shareRide);
            }
            return list;
        }
    }
}
