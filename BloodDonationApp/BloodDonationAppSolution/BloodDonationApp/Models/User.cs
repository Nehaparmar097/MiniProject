using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BloodDonationApp.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        
        [Required]
        public byte[] Password { get; set; }

        [Required]
        public byte[] PasswordHashKey { get; set; }

        [Required]
        public string Role { get; set; }

        public string Status { get; set; } = "Not Activate";
        // Navigation properties
        public Admin Admin { get; set; }
        public Donor Donor { get; set; }
        public Recipient Recipient { get; set; }
       
    }
}
