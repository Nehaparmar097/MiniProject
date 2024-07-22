namespace Job_Portal_API.Models.DTOs
{
    public class UpdateUserDTO
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string UserType { get; set; } // String to match the enum parsing logic
        public string Password { get; set; }
    }
}
