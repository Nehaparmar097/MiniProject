using System.Drawing;

namespace BloodDonationApp.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHashKey { get; set; }
        public Admin Admin { get; set; }
        public Donor Donor { get; set; }
        public Recipient Recipient { get; set; }
    }
}
