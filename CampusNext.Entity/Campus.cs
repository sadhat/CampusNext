using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusNext.Entity
{
    public class Campus : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string CampusCode { get; set; }
        public string LogoUrl { get; set; }
        public int Rank { get; set; }
    }
}
