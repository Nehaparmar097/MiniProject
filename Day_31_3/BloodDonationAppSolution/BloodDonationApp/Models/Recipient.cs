namespace BloodDonationApp.Models
{
    public class Recipient
    {
        public int RecipientId { get; set; }
        public string UserId { get; set; }
        public string BloodType { get; set; }
        public string BloodRequirementDetail { get; set; }

        public User User { get; set; }
        public ICollection<BloodDonation> BloodDonations { get; set; }
    }
}
