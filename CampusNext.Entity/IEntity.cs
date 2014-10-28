using System;
using System.ComponentModel.DataAnnotations;

namespace CampusNext.Entity
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }

    public interface ICampusEntity : IEntity
    {
        string CampusCode { get; set; }
        string UserId { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}