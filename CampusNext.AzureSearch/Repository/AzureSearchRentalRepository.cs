using System;
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
    public class AzureSearchRentalRepository: AzureSearchRepositoryBase
    {
        private const string IndexName = "rental";
        public AzureSearchRentalRepository(string serviceName, string serviceApiKey)
            : base(serviceName, serviceApiKey, IndexName) {}

        public AzureSearchRentalRepository()
            : this(ConfigurationManager.AppSettings["SearchServiceName"], ConfigurationManager.AppSettings["SearchServiceApiKey"])
        {
            
        }

        public override dynamic TransformEntity(IEntity entity)
        {
            var rental = (Rental) entity;
            return new
            {
                id = rental.Id.ToString(CultureInfo.InvariantCulture),
                address = rental.Address,
                rent = rental.Rent,
                rooms = rental.Rooms,
                description = rental.Description,
                updatedDate = DateTime.UtcNow,
                createdDate = DateTime.UtcNow,
                campusCode = rental.CampusCode
            };
        }

        public override async Task<IList<IEntity>> Search(string keyword, string campus = null, IDictionary<string, string> filterDictionary = null)
        {
            var connection = ApiConnection.Create(ServiceName, ServiceApiKey);
            var queryClient = new IndexQueryClient(connection);
            var query = new SearchQuery(keyword + "*");
            
            if(!String.IsNullOrWhiteSpace(campus))
                query.Filter = String.Format("campusCode eq '{0}'", campus);
            if (filterDictionary != null)
            {
                string fromLocation;
                string toLocation;
                string startDateTime;
                string returnDateTime;
                if (filterDictionary.TryGetValue("fromLocation", out fromLocation))
                {
                    query.Filter += String.Format(" and fromLocation gt '{0}'", fromLocation);
                }

                if (filterDictionary.TryGetValue("toLocation", out toLocation))
                {
                    query.Filter += String.Format(" and toLocation gt '{0}'", toLocation);
                }

                if (filterDictionary.TryGetValue("startDateTime", out startDateTime))
                {
                    query.Filter += String.Format(" and startDateTime eq '{0}'", startDateTime);
                }

                if (filterDictionary.TryGetValue("returnDateTime", out returnDateTime))
                {
                    query.Filter += String.Format(" and returnDateTime eq '{0}'", returnDateTime);
                }
            }

            var result = await queryClient.SearchAsync(IndexName, query);
            IList<IEntity> list = new List<IEntity>();
            foreach (var record in result.Body.Records)
            {
                var rental = new Rental
                {
                    Id = int.Parse(record.Properties["id"].ToString()),
                    Address = record.Properties["address"].ToString(),
                    Rent = record.Properties["rent"].ToString(),
                    Rooms = int.Parse(record.Properties["rooms"].ToString()),
                    CampusCode =
                        record.Properties["campusCode"] == null
                            ? string.Empty
                            : record.Properties["campusCode"].ToString(),
                    Description = 
                        record.Properties["description"] == null
                            ? string.Empty
                            : record.Properties["description"].ToString()
                };
                list.Add(rental);
            }
            return list;
        }
    }
}
