using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_API.Models
{
    public class BloodDonation
    {
        public int BloodStockID { get; set; }
        [ForeignKey("BloodStockID")]
        public BloodStock BloodStock { get; set; }
        public int BloodDonationID { get; set; }
        public int RecipientID { get; set; }
        public DateTime DonationDate { get; set; }=DateTime.Now;
       
<<<<<<< HEAD:BloodDonationAPI/BloodDonationAPI/Models/BloodDonation.cs
        public string BloodType { get; set; }
        // Navigation properties
        [ForeignKey("RecipientTD")]
=======
        public string Status { get; set; }  
        // all Navigation properties
        public BloodStock BloodStock { get; set; }
>>>>>>> 67a4622706cd3877fef723c6d8413c8a30dc5df5:BloodDonationApp/BloodDonationApp/BloodDonationApp/Models/BloodDonation.cs
        public Recipient Recipient { get; set; }
    }
}
