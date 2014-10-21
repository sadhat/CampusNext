namespace CampusNext.Entity
{
    public class Profile
    {
        public int Id { get; set; }
        public string FacebookName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }

        public string CampusCode { get; set; }
    }
}