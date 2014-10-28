using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusNext.Entity
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string MessageInJson { get; set; }
    }
}
