using Job_Portal_API.Models.DTOs;
using System.Runtime.CompilerServices;

namespace Job_Portal_API.Interfaces
{
    public interface IBloodDonation
    {
        public Task<BloodDonationResponseDTO> BloodDonation(BloodDonationRequestDTO bloodDonationRequestDTO);

        public Task<IList<BloodDonationResponseDTO>> GetAll();
        public Task<BloodDonationResponseDTO> BloodDonatedTo(int donorId);
    }
}
