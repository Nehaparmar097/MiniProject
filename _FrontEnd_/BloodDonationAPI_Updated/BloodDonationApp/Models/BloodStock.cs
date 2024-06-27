using Job_Portal_API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_API.Models
{
    public class BloodStock
    {
              
        public int ID { get; set; }
        
       public string BloodType { get; set; }
        public string status { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string hospitalName { get; set; }
        public DateTime donationDate { get; set; }
        public int DonorID { get; set; }
        [ForeignKey("DonorID")]
        public Donor Donor { get; set; }




    }
}
