namespace BloodDonationApp.Models
{
    public class Recipient
    {
        public int RecipientId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public string BloodType { get; set; }
        public string BloodRequirementDetail { get; set; }

        public ICollection<BloodStock> BloodStocks { get; set; }
        public ICollection<BloodDonation> BloodDonations { get; set; }
        public ICollection<Hospital> Hospitals { get; set; }
        public string Name { get; internal set; }
    }
}
