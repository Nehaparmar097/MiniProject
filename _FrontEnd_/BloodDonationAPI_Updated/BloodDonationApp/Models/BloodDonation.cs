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
       
        public string BloodType { get; set; }
        // Navigation properties
        [ForeignKey("RecipientTD")]
        public Recipient Recipient { get; set; }
    }
}
