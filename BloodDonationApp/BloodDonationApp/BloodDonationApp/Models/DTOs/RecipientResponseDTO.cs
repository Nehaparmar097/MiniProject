namespace Job_Portal_API.Models.DTOs
{
    public class RecipientResponseDTO
    {
        public int RecipientID { get; set; }
        public int UserID { get; set; }
        public ICollection<RecipientBloodResponseDTO> bloodtype { get; set; }
        
    }
}
