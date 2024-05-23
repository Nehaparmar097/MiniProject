namespace BloodDonationApp.Models
{
    public class Donor
    {
        public int DonorId { get; set; }
        public string UserId { get; set; }
        public string BloodType { get; set; }
        public string DonationStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AvailabilityStatus { get; set; }
        public string PhoneNumber { get; set; }

        public User User { get; set; }
        public ICollection<BloodDonation> BloodDonations { get; set; }
    }
}
