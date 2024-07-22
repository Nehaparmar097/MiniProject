namespace Job_Portal_API.Models.DTOs
{
    public class BloodDonationRequestDTO
    {
      
        public int RecipientID { get; set; }

        public string PreferredState { get; set; }
        public string PreferredCity { get; set; }
        public string PreferredHospital { get; set; }
    }
}
