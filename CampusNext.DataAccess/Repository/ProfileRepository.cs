using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;

namespace CampusNext.DataAccess.Repository
{
    public class ProfileRepository : RepositoryBase
    {
        public Task<IQueryable<Entity.Profile>> Get(string userId)
        {
            var predicate = Predicates.Field<Entity.Profile>(t => t.UserId, Operator.Eq, userId);
            return Task.FromResult(Connection.GetList<Entity.Profile>(predicate).AsQueryable());
        }

        public Task SaveAsync(Entity.Profile profile)
        {
            return Task.FromResult(Connection.Update(profile));
        }

        public Task AddAsync(Entity.Profile profile)
        {
            return Task.FromResult(Connection.Insert(profile));
        }
    }

}
