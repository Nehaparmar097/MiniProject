using Job_Portal_API.Models.Enums;

namespace Job_Portal_API.Models.DTOs
{
    public class BloodStockResponseDTO
    {
        public int ID { get; set; }
        public string BloodType { get; set; }
        public string status { get; set; }
    }
}
