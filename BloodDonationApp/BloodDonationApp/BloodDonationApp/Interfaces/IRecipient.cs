using Job_Portal_API.Models.DTOs;

namespace Job_Portal_API.Interfaces
{
    public interface IRecipient
    {
        
        public Task<RecipientResponseDTO> GetResumeByRecipientId(int recipientID);

        public Task<IEnumerable<RecipientBloodResponseDTO>> AddReciepientBloodAsync(RecipientBloodDTO RecipientBloodDto);
    }
}
