using System;

namespace CampusNext.Entity
{
    public class FindTutor : ICampusEntity
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        public string CampusCode { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}