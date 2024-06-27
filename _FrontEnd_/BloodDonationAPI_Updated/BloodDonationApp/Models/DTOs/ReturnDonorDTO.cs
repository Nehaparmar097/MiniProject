namespace Job_Portal_API.Models.DTOs
{
    public class ReturnDonorDTO
    {
        public int DonorID { get; set; }
        public int UserID { get; set; }
        public int Age { get; set; }

        // Navigation property for the User
       
    }
}
