using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;

namespace CampusNext.DataAccess.Repository
{
    public class RentalRepository : RepositoryBase
    {
        public Task<IQueryable<Entity.Rental>> GetAllFor(string userId)
        {
            var predicate = Predicates.Field<Entity.Rental>(t => t.UserId, Operator.Eq, userId);
            return Task.FromResult(Connection.GetList<Entity.Rental>(predicate).AsQueryable());
        }

        public Task<int> CountAsync(string campusCode)
        {
            var predicate = Predicates.Field<Entity.Rental>(t => t.CampusCode, Operator.Eq, campusCode);
            return Task.FromResult(Connection.GetList<Entity.Rental>(predicate).AsQueryable().Count());
        }

        public Task<Entity.Rental> Get(int id)
        {
            return Task.FromResult(Connection.Get<Entity.Rental>(id));
        }

        public Task AddAsync(Entity.Rental shareRide)
        {
            return Task.FromResult(Connection.Insert(shareRide));
        }

        public Task SaveAsync(Entity.Rental shareRide)
        {
            return Task.FromResult(Connection.Update(shareRide));
        }

        public Task DeleteAsync(Entity.Rental shareRide)
        {
            return Task.FromResult(Connection.Delete(shareRide));
        }

        public IEnumerable<Entity.Rental> Search(string campusCode, string rentalRangeFrom = null, string rentalRangeTo = null, int? rooms = null,
            string additionalInfo = null)
        {
            var pgMain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

            var pg1 = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg1.Predicates.Add(Predicates.Field<Entity.Rental>(r=>r.CampusCode, Operator.Eq, campusCode));
            if(rentalRangeFrom != null)
                pg1.Predicates.Add(Predicates.Field<Entity.Rental>(r => r.Rent, Operator.Ge, rentalRangeFrom));
            if (rentalRangeTo != null)
                pg1.Predicates.Add(Predicates.Field<Entity.Rental>(r => r.Rent, Operator.Le, rentalRangeTo));
            if (rooms != null)
                pg1.Predicates.Add(Predicates.Field<Entity.Rental>(r => r.Rooms, Operator.Eq, rooms));

            pgMain.Predicates.Add(pg1);

            var pg2 = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            if (additionalInfo != null)
            {
                pg2.Predicates.Add(Predicates.Field<Entity.Rental>(r => r.Address, Operator.Like,
                    String.Format("%{0}%", additionalInfo)));
                pg2.Predicates.Add(Predicates.Field<Entity.Rental>(r => r.Description, Operator.Like,
                    String.Format("%{0}%", additionalInfo)));
            }

            pgMain.Predicates.Add(pg2);
            
            return OpenConnection.GetList<Entity.Rental>(pgMain);
        }
    }
}