using System;
using System.Threading.Tasks;
using BloodDonationApp.Exceptions;
using BloodDonationApp.Models.DTOs;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Repositories;

namespace Job_Portal_API.Services
{
    public class RecipientService : IRecipient
    {
        private readonly IRepository<int, Recipient> _recipientRepository;
        private readonly IRepository<int, BloodDonation> _bloodDonationRepository;
        public RecipientService(IRepository<int, Recipient> recipientRepository, IRepository<int, BloodDonation> bloodDonationRepository)
        {
            _recipientRepository = recipientRepository ?? throw new ArgumentNullException(nameof(recipientRepository));
            _bloodDonationRepository = bloodDonationRepository;
        }

        public async Task<RecipientResponseDTO> AddRecipientDetails(AddRecipientDTO recipient)
        {
            try
            {
                // Perform any validation or additional logic here before adding the recipient
                var newRecipient = new Recipient
                {
                    UserID = recipient.UserID,
                    Age = recipient.Age,
                   
                    RequiredBloodType = recipient.RequiredBloodType,
                    BloodRequiredDate = recipient.BloodRequiredDate
                    // Add other properties as needed
                };

                var result = await _recipientRepository.Add(newRecipient);
                return MapRecipientToDTO(result);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and rethrow or return appropriate response
                throw new RecipientServiceException( ex);
            }
        }

        public async Task<RecipientResponseDTO> UpdateAge(int id, int age)
        {
            try
            {
                var recipient = await _recipientRepository.GetById(id);
                if (recipient == null)
                {
                    throw new UserNotFoundException("Recipient not found");
                }

                recipient.Age = age;
                var result = await _recipientRepository.Update(recipient);
                return MapRecipientToDTO(result);
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException(ex);
            }
        }

        public async Task<RecipientResponseDTO> UpdateRequiredBloodType(int id, string requiredBloodType)
        {
            try
            {
                var recipient = await _recipientRepository.GetById(id);
                if (recipient == null)
                {
                    throw new UserNotFoundException("Recipient not found");
                }

                recipient.RequiredBloodType = requiredBloodType;
                var result = await _recipientRepository.Update(recipient);
                return MapRecipientToDTO(result);
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException( ex);
            }
        }

        public async Task<RecipientResponseDTO> UpdateBloodRequiredDate(int id, DateTime bloodRequiredDate)
        {
            try
            {
                var recipient = await _recipientRepository.GetById(id);
                if (recipient == null)
                {
                    throw new UserNotFoundException("Recipient not found");
                }

                recipient.BloodRequiredDate = bloodRequiredDate;
                var result = await _recipientRepository.Update(recipient);
                return MapRecipientToDTO(result);
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException( ex);
            }
        }
        public async Task<IEnumerable<RecipientResponseDTO>> GetAllRecipientsByUserId(int userId)
        {
            try
            {
                var recipients = await _recipientRepository.GetAll();
                var recipientsForUser = recipients.Where(r => r.UserID == userId).Select(MapRecipientToDTO);
                return recipientsForUser;
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException(ex);
            }
        }

        public async Task<RecipientResponseDTO> GetRecipientById(int id)
        {
            try
            {
                var recipient = await _recipientRepository.GetById(id);
                if (recipient == null)
                {
                    throw new UserNotFoundException("Recipient not found");
                }

                return MapRecipientToDTO(recipient);
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException(ex);
            }
        }

        public async Task<IEnumerable<RecipientResponseDTO>> GetRecipientsByRequiredBloodTypeAndDate(string requiredBloodType, DateTime bloodRequiredDate)
        {
            try
            {
                var recipients = await _recipientRepository.GetAll();
                var filteredRecipients = recipients.Where(r => r.RequiredBloodType == requiredBloodType && r.BloodRequiredDate.Date == bloodRequiredDate.Date).Select(MapRecipientToDTO);
                return filteredRecipients;
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException(ex);
            }
        }

        private RecipientResponseDTO MapRecipientToDTO(Recipient recipient)
        {
            return new RecipientResponseDTO
            {
                RecipientID = recipient.RecipientID,
                UserID = recipient.UserID,
                Age = recipient.Age,
                RequiredBloodType = recipient.RequiredBloodType,
                BloodRequiredDate = recipient.BloodRequiredDate
                // Map other properties as needed
            };
        }

        public async Task<IEnumerable<RecipientResponseDTO>> GetRecipientsNotInBloodDonations(int userId)
        {
            try
            {
                var allRecipients = await GetAllRecipientsByUserId(userId);
                var allBloodDonations = await _bloodDonationRepository.GetAll();

                var recipientIdsInBloodDonations = allBloodDonations.Select(b => b.RecipientID).ToHashSet();
                var recipientsNotInBloodDonations = allRecipients
                    .Where(r => !recipientIdsInBloodDonations.Contains(r.RecipientID))
                    .ToList();

                return recipientsNotInBloodDonations;
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException(ex);
            }
        }
        public async Task<IEnumerable<RecipientResponseDTO>> GetRecipientsInBloodDonations(int userId)
        {
            try
            {
                var allRecipients = await GetAllRecipientsByUserId(userId);
                var allBloodDonations = await _bloodDonationRepository.GetAll();

                var recipientIdsInBloodDonations = allBloodDonations.Select(b => b.RecipientID).ToHashSet();
                var recipientsInBloodDonations = allRecipients
                    .Where(r => recipientIdsInBloodDonations.Contains(r.RecipientID))
                    .ToList();

                return recipientsInBloodDonations;
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException(ex);
            }
        }
        public async Task<bool> DeleteRecipientByIdAsync(int id)
        {
            try
            {
                var recipient = await _recipientRepository.GetById(id);
                if (recipient == null)
                {
                    throw new UserNotFoundException("Recipient not found");
                }

                // Perform any additional checks or validations before deletion if needed

                // Delete the recipient
                await _recipientRepository.DeleteById(id);

                return true; // Return true if deletion was successful
            }
            catch (Exception ex)
            {
                throw new RecipientServiceException("Error deleting recipient", ex);
            }
        }


    }

}

