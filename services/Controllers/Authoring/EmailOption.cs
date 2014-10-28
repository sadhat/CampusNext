namespace CampusNext.Services.Controllers.Authoring
{
    public class EmailOption
    {
        public string CategoryName { get; set; }
        public int ItemId { get; set; }
        public string FromEmail { get; set; }
        public string UserId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public string ToEmail { get; set; }
    }
}