using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;

namespace CampusNext.DataAccess.Repository
{
    public class ShareRideRepository : RepositoryBase
    {
        public Task<IQueryable<Entity.ShareRide>> GetAllFor(string userId)
        {
            var predicate = Predicates.Field<Entity.ShareRide>(t => t.UserId, Operator.Eq, userId);
            return Task.FromResult(Connection.GetList<Entity.ShareRide>(predicate).AsQueryable());
        }

        public Task<Entity.ShareRide> Get(int id)
        {
            return Task.FromResult(Connection.Get<Entity.ShareRide>(id));
        }

        public Task AddAsync(Entity.ShareRide shareRide)
        {
            return Task.FromResult(Connection.Insert(shareRide));
        }

        public Task SaveAsync(Entity.ShareRide shareRide)
        {
            return Task.FromResult(Connection.Update(shareRide));
        }

        public Task DeleteAsync(Entity.ShareRide shareRide)
        {
            return Task.FromResult(Connection.Delete(shareRide));
        }
    }
}