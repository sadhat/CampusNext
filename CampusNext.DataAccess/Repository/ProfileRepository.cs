using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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

        public async Task<string> GetEmailBy(string category, int id)
        {
            var sql = String.Format(@"SELECT Email
                                        FROM Profile
                                        JOIN {0} AS Category 
	                                        ON Category.UserId = Profile.UserId
                                        WHERE Category.Id = {1}", category, id);
            var result = await OpenConnection.QueryAsync<String>(sql);
            return result.SingleOrDefault();
        }
    }

}
