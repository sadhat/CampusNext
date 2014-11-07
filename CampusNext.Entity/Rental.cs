using System;
using System.ComponentModel.DataAnnotations;

namespace CampusNext.Entity
{
    public class Rental : ICampusEntity
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string Rent { get; set; }
        public int? Rooms { get; set; }
        public string CampusCode { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int LikeCount { get; set; }
        public string Description { get; set; }
    }
}