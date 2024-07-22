using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;
using Job_Portal_API.Repositories;

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
                BloodStock bloodStock = await _bloodStockRepository.GetById(item.BloodStockID);
                if (bloodStock.DonorID == donorId)
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

       


        public async Task<IList<BloodDonationResponseDTO>> GetAll()
        {
            IList<BloodDonation> bloodDonations = _bloodDonationRepository.GetAll().Result.ToList();
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
        public async Task<BloodDonationResponseDTO> DonateBloodToRecipient(int recipientId)
        {
            /* // Find recipient by ID
             Recipient recipient = await _recipientRepository.GetById(recipientId);
             if (recipient == null)
             {
                 throw new NotFoundException($"Recipient with ID {recipientId} not found.");
             }

             // Get all available blood stocks
             IList<BloodStock> availableBloodStocks = (await _bloodStockRepository.GetAll())
                 .Where(bs => bs.status == "available")
                 .ToList();

             // Find a matching blood stock for the recipient
             BloodStock matchedBloodStock = availableBloodStocks.FirstOrDefault(bs => bs.BloodType == recipient.RequiredBloodType);

             if (matchedBloodStock == null)
             {
                 throw new NotFoundException($"Blood of type {recipient.RequiredBloodType} is not available.");
             }

             // Update the status of the blood stock to unavailable
             matchedBloodStock.status = "unavailable";
             await _bloodStockRepository.Update(matchedBloodStock);

             // Create a new blood donation record
             BloodDonation bloodDonation = new BloodDonation()
             {
                 BloodStockID = matchedBloodStock.ID,
                 RecipientID = recipientId,
                 DonationDate = DateTime.UtcNow, // Use UTC time for donation date
                 BloodType = matchedBloodStock.BloodType,


             };
             bloodDonation = await _bloodDonationRepository.Add(bloodDonation);

             // Prepare the response DTO
             BloodDonationResponseDTO bloodDonationResponse = new BloodDonationResponseDTO()
             {
                 BloodDonationID = bloodDonation.BloodDonationID,
                 RecipientID = bloodDonation.RecipientID,
                 BloodStockID = bloodDonation.BloodStockID,
                 DonationDate = bloodDonation.DonationDate,
                 BloodType = bloodDonation.BloodType,
                 HospitalName = matchedBloodStock.HospitalName, // Fetch from matchedBloodStock
                 City = matchedBloodStock.City,                // Fetch from matchedBloodStock
                 State = matchedBloodStock.State

             };

             return bloodDonationResponse*/
            // Find recipient by ID
            Recipient recipient = await _recipientRepository.GetById(recipientId);
            if (recipient == null)
            {
                throw new NotFoundException($"Recipient with ID {recipientId} not found.");
            }

            // Get all available blood stocks
            IList<BloodStock> availableBloodStocks = (await _bloodStockRepository.GetAll())
                .Where(bs => bs.status == "available")
                .ToList();

            // Find a matching blood stock for the recipient
            BloodStock matchedBloodStock = availableBloodStocks.FirstOrDefault(bs => bs.BloodType == recipient.RequiredBloodType);

            if (matchedBloodStock == null)
            {
                throw new NotFoundException($"Blood of type {recipient.RequiredBloodType} is not available.");
            }

            // Update the status of the blood stock to unavailable
            matchedBloodStock.status = "unavailable";
            await _bloodStockRepository.Update(matchedBloodStock);

            // Create a new blood donation record
            BloodDonation bloodDonation = new BloodDonation()
            {
                BloodStockID = matchedBloodStock.ID,
                RecipientID = recipientId,
                DonationDate = DateTime.UtcNow, // Use UTC time for donation date
                BloodType = matchedBloodStock.BloodType
            };
            bloodDonation = await _bloodDonationRepository.Add(bloodDonation);

           

            // Prepare the response DTO
            BloodDonationResponseDTO bloodDonationResponsse = new BloodDonationResponseDTO()
            {
                BloodDonationID = bloodDonation.BloodDonationID,
                RecipientID = bloodDonation.RecipientID,
                BloodStockID = bloodDonation.BloodStockID,
                DonationDate = bloodDonation.DonationDate,
                BloodType = bloodDonation.BloodType,
                hospitalName = matchedBloodStock.hospitalName, // Fetch from matchedBloodStock
                city = matchedBloodStock.city,                // Fetch from matchedBloodStock
                state = matchedBloodStock.state               // Fetch from matchedBloodStock
            };

            return bloodDonationResponsse;
        }


        public async Task<BloodDonationResponseDTO> DonateBloodToRecipientpre(int recipientId, string preferredState, string preferredCity, string preferredHospital)
        {
            // Find recipient by ID
            Recipient recipient = await _recipientRepository.GetById(recipientId);
            if (recipient == null)
            {
                throw new NotFoundException($"Recipient with ID {recipientId} not found.");
            }
            
            // Get all available blood stocks
            IList<BloodStock> availableBloodStocks = (await _bloodStockRepository.GetAll())
                .Where(bs => bs.status == "available")
                .OrderBy(bs => bs.donationDate) // Order by the earliest available date
                .ToList();
            BloodStock matchedBloodStock = availableBloodStocks.FirstOrDefault(bs => bs.BloodType == recipient.RequiredBloodType);

            // Try to find a matching blood stock for the recipient based on blood type and preferences
           
            if (matchedBloodStock == null)
            {
                // Try to find a matching blood stock based on blood type and any two preferences
                matchedBloodStock = availableBloodStocks.FirstOrDefault(bs =>
                    bs.BloodType == recipient.RequiredBloodType &&
                    ((bs.state == preferredState && bs.city == preferredCity) ||
                     (bs.state == preferredState && bs.hospitalName == preferredHospital) ||
                     (bs.city == preferredCity && bs.hospitalName == preferredHospital)));
            }

            if (matchedBloodStock == null)
            {
                // Try to find a matching blood stock based on blood type and any one preference
                matchedBloodStock = availableBloodStocks.FirstOrDefault(bs =>
                    bs.BloodType == recipient.RequiredBloodType &&
                    (bs.state == preferredState || bs.city == preferredCity || bs.hospitalName == preferredHospital));
            }

            if (matchedBloodStock == null)
            {
                // Find any available blood stock that matches the blood type
                matchedBloodStock = availableBloodStocks.FirstOrDefault(bs => bs.BloodType == recipient.RequiredBloodType);
            }

            if (matchedBloodStock == null)
            {
                throw new NotFoundException($"Blood of type {recipient.RequiredBloodType} is not available.");
            }

            // Update the status of the blood stock to unavailable
            matchedBloodStock.status = "unavailable";
            await _bloodStockRepository.Update(matchedBloodStock);

            // Create a new blood donation record
            BloodDonation bloodDonation = new BloodDonation()
            {
                BloodStockID = matchedBloodStock.ID,
                RecipientID = recipientId,
                DonationDate = DateTime.UtcNow, // Use UTC time for donation date
                BloodType = matchedBloodStock.BloodType
            };
            bloodDonation = await _bloodDonationRepository.Add(bloodDonation);

            BloodDonationResponseDTO bloodDonationResponse = new BloodDonationResponseDTO()
            {
                BloodDonationID = bloodDonation.BloodDonationID,
                RecipientID = bloodDonation.RecipientID,
                BloodStockID = bloodDonation.BloodStockID,
                DonationDate = bloodDonation.DonationDate,
                BloodType = bloodDonation.BloodType,
                hospitalName = matchedBloodStock.hospitalName, // Fetch from matchedBloodStock
                city = matchedBloodStock.city,                // Fetch from matchedBloodStock
                state = matchedBloodStock.state
            };

            return bloodDonationResponse;
        }

        /* public void DeleteBloodDonation(int donationId)
         {
             var donation = _bloodDonationRepository.GetById(donationId).Result;
             if (donation != null)
             {
                 _bloodDonationRepository.Delete(donationId);

                 // Delete recipient if associated
                 if (donation.RecipientID != null)
                 {
                     var recipient = _recipientRepository.GetById((int)donation.RecipientID).Result;
                     if (recipient != null)
                     {
                         _recipientRepository.Delete((int)donation.RecipientID);
                     }
                 }

                 // Set blood stock availability status to available
                 var bloodStock = _bloodStockRepository.GetById(donation.BloodStockID).Result;
                 if (bloodStock != null)
                 {
                     bloodStock.status = "available";
                     _bloodStockRepository.Update(bloodStock);
                 }
             }
             else
             {
                 throw new InvalidOperationException("Blood donation not found.");
             }
         }
        */
        public async Task<BloodDonationResponseDTO> DonateBloodToRecipient(int recipientId, string preferredState, string preferredCity, string preferredHospital)
    {
        // Find recipient by ID
        Recipient recipient = await _recipientRepository.GetById(recipientId);
        if (recipient == null)
        {
            throw new NotFoundException($"Recipient with ID {recipientId} not found.");
        }

        // Get all available blood stocks
        IList<BloodStock> availableBloodStocks = (await _bloodStockRepository.GetAll())
            .Where(bs => bs.status == "available")
            .OrderBy(bs => bs.donationDate) // Order by the earliest available date
            .ToList();

        // Try to find a matching blood stock for the recipient based on blood type and preferences
        BloodStock matchedBloodStock = availableBloodStocks.FirstOrDefault(bs =>
            bs.BloodType == recipient.RequiredBloodType &&
            (bs.state == preferredState || bs.city == preferredCity || bs.hospitalName == preferredHospital));

        if (matchedBloodStock == null)
        {
            // Try to find a matching blood stock based on blood type and any two preferences
            matchedBloodStock = availableBloodStocks.FirstOrDefault(bs =>
                bs.BloodType == recipient.RequiredBloodType &&
                ((bs.state == preferredState && bs.city == preferredCity) ||
                 (bs.state == preferredState && bs.hospitalName == preferredHospital) ||
                 (bs.city == preferredCity && bs.hospitalName == preferredHospital)));
        }

        if (matchedBloodStock == null)
        {
            // Try to find a matching blood stock based on blood type and any one preference
            matchedBloodStock = availableBloodStocks.FirstOrDefault(bs =>
                bs.BloodType == recipient.RequiredBloodType &&
                (bs.state == preferredState || bs.city == preferredCity || bs.hospitalName == preferredHospital));
        }

        if (matchedBloodStock == null)
        {
            // Find any available blood stock that matches the blood type
            matchedBloodStock = availableBloodStocks.FirstOrDefault(bs => bs.BloodType == recipient.RequiredBloodType);
        }

        if (matchedBloodStock == null)
        {
            throw new NotFoundException($"Blood of type {recipient.RequiredBloodType} is not available.");
        }

        // Update the status of the blood stock to unavailable
        matchedBloodStock.status = "unavailable";
        await _bloodStockRepository.Update(matchedBloodStock);

        // Create a new blood donation record
        BloodDonation bloodDonation = new BloodDonation()
        {
            BloodStockID = matchedBloodStock.ID,
            RecipientID = recipientId,
            DonationDate = DateTime.UtcNow, // Use UTC time for donation date
            BloodType = matchedBloodStock.BloodType
        };
        bloodDonation = await _bloodDonationRepository.Add(bloodDonation);

        // Prepare the response DTO
        BloodDonationResponseDTO bloodDonationResponse = new BloodDonationResponseDTO()
        {
            BloodDonationID = bloodDonation.BloodDonationID,
            RecipientID = bloodDonation.RecipientID,
            BloodStockID = bloodDonation.BloodStockID,
            DonationDate = bloodDonation.DonationDate,
            BloodType = bloodDonation.BloodType
        };

        return bloodDonationResponse;
    }

        public Task<BloodDonationResponseDTO> BloodDonation(BloodDonationRequestDTO bloodDonationRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}