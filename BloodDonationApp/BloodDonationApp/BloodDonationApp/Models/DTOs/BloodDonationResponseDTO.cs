namespace Job_Portal_API.Models.DTOs
{
    public class BloodDonationResponseDTO
    {
        public int BloodDonationID { get; set; }
        public int ID { get; set; }
        public int RecipientID { get; set; }
        public DateTime DonationDate { get; set; }
        public string Status { get; set; }
      
    }
}
