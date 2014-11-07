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
    public class AzureSearchFindTutorRepository: AzureSearchRepositoryBase
    {
        private const string IndexName = "findtutor";

        public AzureSearchFindTutorRepository(string serviceName, string serviceApiKey)
            : base(serviceName, serviceApiKey, IndexName) {}

        public AzureSearchFindTutorRepository()
            : this(ConfigurationManager.AppSettings["SearchServiceName"], ConfigurationManager.AppSettings["SearchServiceApiKey"])
        {
            
        }

        public override dynamic TransformEntity(IEntity entity)
        {
            var findTutor = (FindTutor) entity;
            return new
            {
                id = findTutor.Id.ToString(CultureInfo.InvariantCulture),
                description = findTutor.Description,
                rate = findTutor.Rate,
                course = findTutor.Course,
                updatedDate = DateTime.UtcNow,
                createdDate = DateTime.UtcNow,
                campusCode = findTutor.CampusCode
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
                var findTutor = new FindTutor
                {
                    Id = int.Parse(record.Properties["id"].ToString()),
                    Description = record.Properties["description"].ToString(),
                    Rate = record.Properties["rate"] == null ? string.Empty : record.Properties["rate"].ToString(),
                    Course =
                        record.Properties["course"] == null
                            ? string.Empty
                            : record.Properties["course"].ToString()
                };
                list.Add(findTutor);
            }
            return list;
        }
    }
}
