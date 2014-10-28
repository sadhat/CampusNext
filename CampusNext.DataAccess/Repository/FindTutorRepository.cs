using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;

namespace CampusNext.DataAccess.Repository
{
    public class FindTutorRepository : RepositoryBase
    {

        public Task<IQueryable<Entity.FindTutor>> GetAllFor(string userId)
        {
            var predicate = Predicates.Field<Entity.FindTutor>(t => t.UserId, Operator.Eq, userId);
            return Task.FromResult(Connection.GetList<Entity.FindTutor>(predicate).AsQueryable());
        }

        public Task<Entity.FindTutor> Get(int id)
        {
            return Task.FromResult(Connection.Get<Entity.FindTutor>(id));
        }

        public Task AddAsync(Entity.FindTutor tutor)
        {
            return Task.FromResult(Connection.Insert(tutor));
        }

        public Task SaveAsync(Entity.FindTutor tutor)
        {
            return Task.FromResult(Connection.Update(tutor));
        }

        public Task DeleteAsync(Entity.FindTutor tutor)
        {
            return Task.FromResult(Connection.Delete(tutor));
        }
    }
}