namespace Job_Portal_API.Models
{
    public class BloodDonation
    {
        public int BloodDonationID { get; set; }
        public int ID { get; set; }
        public int RecipientID { get; set; }
        public DateTime DonationDate { get; set; }=DateTime.Now;
       
        public string Status { get; set; }  
        // all Navigation properties
        public BloodStock BloodStock { get; set; }
        public Recipient Recipient { get; set; }
    }
}
