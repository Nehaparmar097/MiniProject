namespace BloodDonationApp.Models.DTOs
{
    public interface UserRegisterReturnDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
    }
}
