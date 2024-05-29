using Job_Portal_API.Models.DTOs;
using System.Runtime.CompilerServices;

namespace Job_Portal_API.Interfaces
{
    public interface IBloodDonation
    {
        public Task<BloodDonationResponseDTO> BlooddonationCompleted(int ID,int RecipientId);

        public Task<BloodDonationStatusDTO> GetBloodDonationStatus(int BloodDonationId);
        public Task<IEnumerable<BloodDonationResponseDTO>> GetBloodDonationByRecipientID(int RecipientID);
    }
}
