namespace Job_Portal_API.Models.DTOs
{
    public class RecipientResponseDTO
    {
        public int RecipientID { get; set; }
        public int UserID { get; set; }
        public int Age { get; set; }

        public string RequiredBloodType { get; set; }
        // Navigation property for the User
        public DateTime BloodRequiredDate { get; set; }

    }
}
