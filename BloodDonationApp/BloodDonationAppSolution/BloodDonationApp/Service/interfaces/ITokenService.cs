using BloodDonationApp.Models;

namespace BloodDonationApp.Service.interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
