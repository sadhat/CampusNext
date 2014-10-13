using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CampusNext.DataAccess.Entities;
using DapperExtensions;

namespace CampusNext.DataAccess
{
    public abstract class RepositoryBase
    {
        protected IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["CampusNextContext"].ConnectionString);
            }
        }
    }
    public class TextbookRepository : RepositoryBase
    {
        public Task<IQueryable<Textbook>> GetAllFor(string userId)
        {
            var predicate = Predicates.Field<Textbook>(t => t.UserId, Operator.Eq, userId);
            return Task.FromResult(Connection.GetList<Textbook>(predicate).AsQueryable());
        }

        public Task<Textbook> Get(int id)
        {
            return Task.FromResult(Connection.Get<Textbook>(id));
        }

        public Task AddAsync(Textbook textbook)
        {
            return Task.FromResult(Connection.Insert(textbook));
        }

        public Task SaveAsync(Textbook textbook)
        {
            return Task.FromResult(Connection.Update(textbook));
        }

        public Task DeleteAsync(Textbook textbook)
        {
            return Task.FromResult(Connection.Delete(textbook));
        }

    }
}