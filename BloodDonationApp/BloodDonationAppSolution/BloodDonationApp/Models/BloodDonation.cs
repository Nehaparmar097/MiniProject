using System.Drawing;

namespace BloodDonationApp.Models
{
    public class BloodDonation
    {
        public int BloodDonationId { get; set; }
        public int DonorId { get; set; }
        public Donor Donor { get; set; }
        public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }
        public DateTime DonationDate { get; set; }
        public string DonationStatus { get; set; }
    }
}
