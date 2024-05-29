namespace Job_Portal_API.Models
{
    public class Donor
    {
        
        
            public int DonorID { get; set; }
            public int UserID { get; set; }
            public string HospitalName { get; set; }
            public string HospitalDescription { get; set; }
            public string HospitalLocation { get; set; }

            // Navigation property for the User
            public User User { get; set; }
            public ICollection<BloodStock> BloodStocks { get; set; }


    }
}
