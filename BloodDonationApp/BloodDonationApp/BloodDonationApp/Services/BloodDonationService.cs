using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;

namespace Job_Portal_API.Services
{
    public class BloodDonationService : IBloodDonation
    {
        private readonly IRepository<int, BloodDonation> _bloodDonationRepository;
        private readonly IRepository<int, BloodStock> _bloodStockRepository;
        private readonly IRepository<int, Recipient> _jobSeekerRepository;

        public BloodDonationService(
            IRepository<int, BloodDonation> bloodDonationRepository,
            IRepository<int, BloodStock> bloodStockRepository,
            IRepository<int, Recipient> jobSeekerRepository)
        {
            _bloodDonationRepository = bloodDonationRepository;
            _bloodStockRepository = bloodStockRepository;
            _jobSeekerRepository = jobSeekerRepository;
        }
        public async  Task<BloodDonationResponseDTO> BlooddonationCompleted(int ID, int recipientID)
        {

            try
            {
                var bloodDonations = await _bloodDonationRepository.GetAll();
                var existBloodDonation = bloodDonations.Where(ap => ap.RecipientID == recipientID && ap.ID == ID);
                if (existBloodDonation != null) throw new BloodDonationAlreadyExistException("BloodDonation Already Exist");
                var bloodStock = await _bloodStockRepository.GetById(ID);
                var jobSeeker = await _jobSeekerRepository.GetById(recipientID);
                var bloodDonation = new BloodDonation
                {
                    ID = ID,
                    RecipientID = recipientID,
                    Status = "Pending"
                };

                var addedBloodDonation = await _bloodDonationRepository.Add(bloodDonation);
                return await MapToDTO(addedBloodDonation);

            }
            catch (BloodStockNotFoundException e) {
                throw new BloodStockNotFoundException(e.Message);
            }
            catch (RecipientNotFoundException e)
            {
                throw new RecipientNotFoundException(e.Message);
            }
            catch(BloodDonationAlreadyExistException e)
            {
                throw new BloodDonationAlreadyExistException(e.Message);
            }


            
           
           
        }
        public async Task<BloodDonationResponseDTO> MapToDTO(BloodDonation bloodDonation)
        {
            return new BloodDonationResponseDTO
            {
                BloodDonationID = bloodDonation.BloodDonationID,
                ID = bloodDonation.ID,
                RecipientID = bloodDonation.RecipientID,
                Status = bloodDonation.Status
            };
        }

        public async  Task<BloodDonationStatusDTO> GetBloodDonationStatus(int bloodDonationId)
        {
            try
            {
                var bloodDonation = await _bloodDonationRepository.GetById(bloodDonationId);
                var result = new BloodDonationStatusDTO
                {
                    BloodDonationID = bloodDonation.BloodDonationID,
              
                    Status = bloodDonation.Status
                };
                return result;
            }
            catch(BloodDonationNotFoundException e)
            {
                throw new BloodDonationNotFoundException(e.Message);
            }
        }
        public async Task<IEnumerable<BloodDonationResponseDTO>> GetBloodDonationByRecipientID(int recipientID)
        {
            try
            {
                var jobSeeker = await _jobSeekerRepository.GetById(recipientID);
                var bloodDonations = await _bloodDonationRepository.GetAll();
                if(!bloodDonations.Any()) throw new BloodDonationNotFoundException();
                var filteredBloodDonations = bloodDonations.Where(bloodDonation => bloodDonation.RecipientID == recipientID);
                if(!filteredBloodDonations.Any()) throw new BloodDonationNotFoundException($"No BloodDonation Found for the Given RecipientID {recipientID}");
                return filteredBloodDonations.Select(bloodDonation => new BloodDonationResponseDTO
                {
                    BloodDonationID = bloodDonation.BloodDonationID,
                    ID = bloodDonation.ID,
                    RecipientID = bloodDonation.RecipientID,
                    Status = bloodDonation.Status,
                    DonationDate = bloodDonation.DonationDate
                });
            }
            catch (RecipientNotFoundException e) {
                throw new RecipientNotFoundException(e.Message);
            }
            catch (BloodDonationNotFoundException e)
            {
                throw new BloodDonationNotFoundException(e.Message);
            }

        }
    }
}
