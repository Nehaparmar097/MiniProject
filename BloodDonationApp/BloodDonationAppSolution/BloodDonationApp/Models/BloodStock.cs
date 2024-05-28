namespace BloodDonationApp.Models
{
    public class BloodStock
    {

        public int BloodStockId { get; set; }
        public string BloodType { get; set; }
        public int Volume { get; set; }

        public int? AdminId { get; set; }
        public Admin Admin { get; set; }

        public int? DonorId { get; set; }
        public Donor Donor { get; set; }

        public int? RecipientId { get; set; }
        public Recipient Recipient { get; set; }
    }
}
