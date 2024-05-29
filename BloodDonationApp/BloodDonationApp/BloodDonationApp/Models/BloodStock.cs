﻿using Job_Portal_API.Models.Enums;

namespace Job_Portal_API.Models
{
    public class BloodStock
    {
        
        
            public int ID { get; set; }
        public string Disease_Title { get; set; }
        public string Disease_Description { get; set; }
        public string BloodType { get; set; }
        public Blood_Type status { get; set; }
            public string Location { get; set; }
            public double Charge { get; set; }
            public DateTime donationDate { get; set; }
            public DateTime expiryDate { get; set; }
            public int DonorID { get; set; }

            // Navigation property for the related Employer entity
            public Donor Donor { get; set; }
            public ICollection<BloodDonation> BloodDonations { get; set; }

            public ICollection<DonorBlood> DonorBloods { get; set; }




    }
}
