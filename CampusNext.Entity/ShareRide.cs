using System;
using System.ComponentModel.DataAnnotations;

namespace CampusNext.Entity
{
    public class ShareRide : ICampusEntity
    {
        [Key]
        public int Id { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public bool IsRoundTrip { get; set; }
        public string StartDateTime { get; set; }
        public string ReturnDateTime { get; set; }
        public string CampusCode { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int LikeCount { get; set; }
        public string AdditionalInfo { get; set; }
    }
}