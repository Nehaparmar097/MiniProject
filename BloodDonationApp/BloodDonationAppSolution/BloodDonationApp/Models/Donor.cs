namespace BloodDonationApp.Models
{
    public class Donor
    {
        public int DonorId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string BloodType { get; set; }
        public string DonationStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AvailabilityStatus { get; set; }

        public ICollection<BloodStock> BloodStocks { get; set; }
        
        public ICollection<Hospital> Hospitals { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
    }
}
