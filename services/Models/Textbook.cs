using System.ComponentModel.DataAnnotations;

namespace CampusNext.Services.Models
{
    public class Textbook
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}