using BloodDonationApp.Models.DTOs;
using Job_Portal_API.Models.DTOs;

namespace Job_Portal_API.Interfaces
{
    public interface IRecipient
    {
        Task<RecipientResponseDTO> AddRecipientDetails(AddRecipientDTO recipient);
        Task<RecipientResponseDTO> UpdateAge(int id, int age);
        Task<RecipientResponseDTO> UpdateRequiredBloodType(int id, string requiredBloodType);
        Task<RecipientResponseDTO> UpdateBloodRequiredDate(int id, DateTime bloodRequiredDate);
    }

}

