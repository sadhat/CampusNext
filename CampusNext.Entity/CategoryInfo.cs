using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusNext.Entity
{
    public class CategoryInfo : IEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Id { get; set; }
    }
}
