namespace BloodDonationApp.Models.DTOs
{
    public interface UserRegisterReturnDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

    }
}
