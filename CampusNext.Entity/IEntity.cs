using System.ComponentModel.DataAnnotations;

namespace CampusNext.Entity
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}