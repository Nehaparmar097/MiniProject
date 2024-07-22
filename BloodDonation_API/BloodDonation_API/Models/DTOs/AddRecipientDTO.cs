namespace BloodDonationApp.Models.DTOs
{
   
        public class AddRecipientDTO
        {
            public int UserID { get; set; }
            public int Age { get; set; }
            public string RequiredBloodType { get; set; }
            public DateTime BloodRequiredDate { get; set; }
            // Add other properties as needed
        }
    }

