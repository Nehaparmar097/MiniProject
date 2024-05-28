using BloodDonationApp.Models.DTOs;
using BloodDonationApp.Models;

namespace BloodDonationApp.Repository.Interfaces
{
    public interface IUserRegisterRepository
    {
        Task<(Recipient recipient, User user)> AddRecipientUserWithTransaction(UserRegisterRepositoryDTO userRegisterDTO);
        Task<(Donor donor, User user)> AddDonorUserWithTransaction(UserRegisterRepositoryDTO userRegisterDTO);
    }
}
