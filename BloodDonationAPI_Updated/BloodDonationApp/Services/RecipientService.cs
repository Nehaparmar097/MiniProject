using System;
using System.Threading.Tasks;
using BloodDonationApp.Exceptions;
using BloodDonationApp.Models.DTOs;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;

namespace Job_Portal_API.Services
{
    public class RecipientService : IRecipient
    {
        private readonly IRepository<int, Recipient> _recipientRepository;

        public RecipientService(IRepository<int, Recipient> recipientRepository)
        {
            _recipientRepository = recipientRepository ?? throw new ArgumentNullException(nameof(recipientRepository));
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
    }
}
