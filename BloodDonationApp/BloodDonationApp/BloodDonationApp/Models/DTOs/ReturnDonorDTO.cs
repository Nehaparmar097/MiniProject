namespace Job_Portal_API.Models.DTOs
{
    public class ReturnDonorDTO
    {
        public int DonorID { get; set; }
        public int UserID { get; set; }
        public string HospitalName { get; set; }
        public string HospitalDescription { get; set; }
        public string HospitalLocation { get; set; }
    }
}
