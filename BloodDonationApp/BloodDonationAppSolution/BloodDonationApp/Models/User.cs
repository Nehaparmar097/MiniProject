using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BloodDonationApp.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public UserRole Role { get; set; }
        // Navigation properties
        public Admin Admin { get; set; }
        public Donor Donor { get; set; }
        public Recipient Recipient { get; set; }
    }
}
