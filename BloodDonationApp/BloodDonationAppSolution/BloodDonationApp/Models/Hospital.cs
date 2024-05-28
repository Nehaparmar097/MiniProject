namespace BloodDonationApp.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }

        public ICollection<Donor> Donors { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
    }
}
