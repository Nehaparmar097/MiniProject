using BloodDonationApp.Models.DTOs;

namespace BloodDonationApp.Interfaces
{
    public interface IUserService
    {
        public Task<LoginReturnDTO> Login(UserLoginDTO userLoginDTO);
        public Task<UserRegisterReturnDTO> Register(UserRegisterDTO userRegisterDTO);
    }
}
