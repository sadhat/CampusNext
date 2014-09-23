using System;
using System.ComponentModel.DataAnnotations;

namespace CampusNext.Services.Models
{
    public class Textbook
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CampusName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}