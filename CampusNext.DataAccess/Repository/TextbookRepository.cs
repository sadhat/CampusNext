using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;

namespace CampusNext.DataAccess.Repository
{
    public class TextbookRepository : RepositoryBase
    {
        public Task<IQueryable<Entity.Textbook>> GetAllFor(string userId)
        {
            var predicate = Predicates.Field<Entity.Textbook>(t => t.UserId, Operator.Eq, userId);
            return Task.FromResult(Connection.GetList<Entity.Textbook>(predicate).AsQueryable());
        }

        public Task<Entity.Textbook> Get(int id)
        {
            return Task.FromResult(Connection.Get<Entity.Textbook>(id));
        }

        public Task AddAsync(Entity.Textbook textbook)
        {
            return Task.FromResult(Connection.Insert(textbook));
        }

        public Task SaveAsync(Entity.Textbook textbook)
        {
            return Task.FromResult(Connection.Update(textbook));
        }

        public Task DeleteAsync(Entity.Textbook textbook)
        {
            return Task.FromResult(Connection.Delete(textbook));
        }

    }
}