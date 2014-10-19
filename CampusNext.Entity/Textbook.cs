using System;
using System.ComponentModel.DataAnnotations;

namespace CampusNext.Entity
{
    public class Textbook : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Authors { get; set; }
        public string Isbn { get; set; }
        public string Course { get; set; }
        public double Price { get; set; }
        public string CurrencyType { get; set; }
        public string CampusCode { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int LikeCount { get; set; }
        public string ContactInfo { get; set; }
    }
}