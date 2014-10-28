using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusNext.Entity;
using DapperExtensions;

namespace CampusNext.DataAccess.Repository
{
    public class MessageRepository : RepositoryBase
    {
        public Task<bool> AddAsync(string messageInJson)
        {
            var message = new Message
            {
                MessageInJson = messageInJson
            };
            Connection.Insert(message);
            return Task.FromResult(true);
        }
    }
}
