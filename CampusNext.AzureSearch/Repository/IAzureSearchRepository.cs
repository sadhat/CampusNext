using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureSearchClient;
using CampusNext.Entity;

namespace CampusNext.AzureSearch.Repository
{
    public interface IAzureSearchRepository
    {
        Task Add(IEntity entity);
        Task Delete(IEntity entity);
        Task Update(IEntity entity);
        Task<int> Count();
        Task<T> Get<T>(string key);

        Task<IList<IEntity>> Search(string keyword, string campus = null, IDictionary<string, string> filterDictionary = null);
    }
}
