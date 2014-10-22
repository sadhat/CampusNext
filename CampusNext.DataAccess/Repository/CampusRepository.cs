using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;

namespace CampusNext.DataAccess.Repository
{
    public class CampusRepository : RepositoryBase
    {
        public Task<IQueryable<Entity.Campus>> All()
        {
            return Task.FromResult(Connection.GetList<Entity.Campus>().AsQueryable());
        }
    }
}
