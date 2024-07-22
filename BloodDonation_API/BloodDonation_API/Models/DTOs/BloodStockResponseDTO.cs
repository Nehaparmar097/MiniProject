using Job_Portal_API.Models.Enums;

namespace Job_Portal_API.Models.DTOs
{
    public class BloodStockResponseDTO
    {
        public int ID { get; set; }
        public string BloodType { get; set; }
        public string status { get; set; }
        public string hospitalName { get; set; }
        public string city { get; set; }
        public string state { get; set; }
       

        // public DateTime donationDate { get; set; } = DateTime.Now();
    }
}
