using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

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
        public IQueryable<Textbook> GetTextbook(string userId)
        {
            return Connection.Query<Textbook>("SELECT * FROM Textbook WHERE userId = @id", new {id = userId}).AsQueryable();
        }

        public async Task AddAsync(Textbook textbook)
        {
            using (var context = new CampusNextContext())
            {
                context.Entry(textbook).State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(Textbook textbook)
        {
            using (var context = new CampusNextContext())
            {
                context.Entry(textbook).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Textbook textbook)
        {
            using (var context = new CampusNextContext())
            {
                context.Entry(textbook).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

    }
}