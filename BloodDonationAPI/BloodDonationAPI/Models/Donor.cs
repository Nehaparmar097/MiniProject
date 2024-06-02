namespace Job_Portal_API.Models
{
    public class Donor
    {
        
        
            public int DonorID { get; set; }
            public int UserID { get; set; }
            public int Age { get; set; }
            
            // Navigation property for the User
            public User User { get; set; }
            public ICollection<BloodStock> BloodStocks { get; set; }


    }
}
