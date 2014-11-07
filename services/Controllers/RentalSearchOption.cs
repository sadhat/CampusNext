namespace CampusNext.Services.Controllers
{
    public class RentalSearchOption
    {
        public string CampusName { get; set; }
        public string RentRangeFrom { get; set; }
        public string RentRangeTo { get; set; }
        public int? Rooms { get; set; }
        public string AdditionalInfo { get; set; }
    }
}