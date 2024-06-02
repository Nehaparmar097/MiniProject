namespace Job_Portal_API.Models
{
    public class Recipient
    {
        
        
            public int RecipientID { get; set; }
            public int UserID { get; set; }
            public int  Age { get; set; }

            public string RequiredBloodType { get; set; }
        // Navigation property for the User
             public DateTime BloodRequiredDate { get; set; }
            public User User { get; set; }

            public ICollection<BloodDonation> BloodDonations { get; set; }
            public ICollection<RecipientBlood> RecipientBloods { get; set; }
            
            





    }
}
