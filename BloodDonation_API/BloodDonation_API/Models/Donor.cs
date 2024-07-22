using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_API.Models
{
    public class Donor
    {
        
        
            public int DonorID { get; set; }
            public int UserID { get; set; }
            public int Age { get; set; }

        // Navigation property for the User
        [ForeignKey("UserID")]
            public User User { get; set; }
            public ICollection<BloodStock> BloodStocks { get; set; }


    }
}
