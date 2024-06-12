using Job_Portal_API.Models.Enums;

namespace Job_Portal_API.Models.DTOs
{
    public class BloodStockDTO
    { 

        public string BloodType { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string hospitalName { get; set; }
        public int DonorID { get; set; }
    }
}
