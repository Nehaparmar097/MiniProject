namespace Job_Portal_API.Models
{
    public class RecipientBlood
    {   public int RecipientBloodID { get; set; }
        public int RecipientID { get; set; }
       public string bloodtype { get; set; }    

        // Navigation properties
        public Recipient Recipient { get; set; }
       
    }
}
