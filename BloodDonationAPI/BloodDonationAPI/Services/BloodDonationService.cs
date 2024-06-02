using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;

namespace Job_Portal_API.Services
{
    public class BloodDonationService : IBloodDonation
    {
        private readonly IRepository<int, BloodDonation> _bloodDonationRepository;
        private readonly IRepository<int, BloodStock> _bloodStockRepository;
        private readonly IRepository<int, Recipient> _recipientRepository;

        public BloodDonationService(
            IRepository<int, BloodDonation> bloodDonationRepository,
            IRepository<int, BloodStock> bloodStockRepository,
            IRepository<int, Recipient> recipientRepository)
        {
            _bloodDonationRepository = bloodDonationRepository;
            _bloodStockRepository = bloodStockRepository;
            _recipientRepository = recipientRepository;
        }

        public async Task<BloodDonationResponseDTO> BloodDonatedTo(int donorId)
        {           
            IList<BloodDonation> bloodDonations = _bloodDonationRepository.GetAll().Result.ToList();
            BloodDonationResponseDTO bloodDonationResponseDTO = new BloodDonationResponseDTO();
            foreach (var item in bloodDonations)
            {
                BloodStock bloodStock= await _bloodStockRepository.GetById(item.BloodStockID);
                if(bloodStock.DonorID== donorId)
                {
                    bloodDonationResponseDTO.BloodDonationID = item.BloodDonationID;
                    bloodDonationResponseDTO.RecipientID = item.RecipientID;
                    bloodDonationResponseDTO.BloodStockID = item.BloodStockID;
                    bloodDonationResponseDTO.DonationDate = item.DonationDate;
                    bloodDonationResponseDTO.BloodType = item.BloodType;
                }
            }
            return bloodDonationResponseDTO;
        }

        public async Task<BloodDonationResponseDTO> BloodDonation(BloodDonationRequestDTO bloodDonationRequestDTO)
        {
            BloodStock bloodStock= await _bloodStockRepository.GetById(bloodDonationRequestDTO.BloodStockID);
            BloodDonation bloodDonation = new BloodDonation()
            {
                BloodStockID = bloodDonationRequestDTO.BloodStockID,
                RecipientID = bloodDonationRequestDTO.RecipientID,
                DonationDate = new DateTime(),
                BloodType= bloodStock.BloodType
            };
            bloodDonation= await _bloodDonationRepository.Add(bloodDonation);
            bloodStock.status = "unavailable";
            await _bloodStockRepository.Update(bloodStock);
            BloodDonationResponseDTO bloodDonationResponse = new BloodDonationResponseDTO()
            {
                BloodDonationID=bloodDonation.BloodDonationID,
                RecipientID=bloodDonation.RecipientID,
                BloodStockID=bloodDonation.BloodStockID,
                DonationDate=bloodDonation.DonationDate,
                BloodType=bloodDonation.BloodType
            };
            return bloodDonationResponse;
        }

        public async Task<IList<BloodDonationResponseDTO>> GetAll()
        {
            IList<BloodDonation> bloodDonations =  _bloodDonationRepository.GetAll().Result.ToList();
            IList<BloodDonationResponseDTO> responseDTOs = new List<BloodDonationResponseDTO>();
            foreach (var bloodDonation in bloodDonations)
            {
                BloodDonationResponseDTO bloodDonationResponse = new BloodDonationResponseDTO()
                {
                    BloodDonationID = bloodDonation.BloodDonationID,
                    RecipientID = bloodDonation.RecipientID,
                    BloodStockID = bloodDonation.BloodStockID,
                    DonationDate = bloodDonation.DonationDate,
                    BloodType = bloodDonation.BloodType
                };
                responseDTOs.Add(bloodDonationResponse);
            }
            return responseDTOs;
        }
    }
}
