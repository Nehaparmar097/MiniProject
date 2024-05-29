namespace Job_Portal_API.Models.DTOs
{
    public class AddDonorDTO
    {
        public int UserID { get; set; }
        public string HospitalName { get; set; }
        public string HospitalDescription { get; set; }
        public string HospitalLocation { get; set; }
    }
}
