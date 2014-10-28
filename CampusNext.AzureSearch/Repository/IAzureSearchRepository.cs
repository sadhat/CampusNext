using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureSearchClient;
using CampusNext.Entity;

namespace CampusNext.AzureSearch.Repository
{
    public interface IAzureSearchRepository
    {
        Task<HttpResponseMessage> AddAsync(IEntity entity);
        Task<HttpResponseMessage> DeleteAsync(IEntity entity);
        Task<HttpResponseMessage> UpdateAsync(IEntity entity);
        Task<int> Count();
        Task<int> Count(string indexName, string campusCode);
        Task<T> Get<T>(string key);
        RedDog.Search.Model.Index GetIndex(string indexName);
        Task<IList<IEntity>> Search(string keyword, string campus = null, IDictionary<string, string> filterDictionary = null);
    }
}
