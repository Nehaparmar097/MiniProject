namespace Job_Portal_API.Models.DTOs
{
    public class BloodDonationResponseDTO
    {
        public int BloodDonationID { get; set; }
        public int BloodStockID { get; set; }
        public int RecipientID { get; set; }
        public DateTime DonationDate { get; set; }
        public string hospitalName { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string BloodType { get; set; }
      
    }
}
