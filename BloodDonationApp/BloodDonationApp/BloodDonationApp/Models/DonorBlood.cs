namespace Job_Portal_API.Models
{
    public class DonorBlood
    {   public int DonorBloodID { get; set; }
        public int ID { get; set; }
        public string bloodtype { get; set; }   

        // Navigation properties
        public BloodStock BloodStocks { get; set; }
         
        
    }
}
