using BloodDonationApp.Models.DTOs;

namespace BloodDonationApp.Service.interfaces
{
    public interface IUserService
    {
        Task<RegisterReturnDTO> RecipientRegister(UserRegisterDTO userRegisterDTO);
        Task<LoginReturnDTO> RecipientLogin(UserLoginDTO userLoginDTO);
        Task<RegisterReturnDTO> DonorRegister(UserRegisterDTO userRegisterDTO);
        Task<LoginReturnDTO> DonorLogin(UserLoginDTO userLoginDTO);
    }
}
