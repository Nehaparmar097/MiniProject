using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using BloodDonationApp.Exceptions;

namespace Job_Portal_API.Services
{
    public class DonorService : IDonor
    {
        private readonly IRepository<int, Donor> _donorRepository;

        public DonorService(IRepository<int, Donor> donorRepository)
        {
            _donorRepository = donorRepository ?? throw new ArgumentNullException(nameof(donorRepository));
        }

        public async Task<ReturnDonorDTO> AddDonorDetails(AddDonorDTO donor)
        {
            try
            {
                // Perform any validation or additional logic here before adding the donor
                var newDonor = new Donor
                {
                    UserID = donor.UserID,
                    Age = donor.Age
                    // Add other properties as needed
                };

                var result = await _donorRepository.Add(newDonor);
                return MapDonorToDTO(result);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and rethrow or return appropriate response
                throw new DonorServiceException(ex);
            }
        }

        public async Task<ReturnDonorDTO> UpdateAge(int id, int age)
        {
            try
            {
                var donor = await _donorRepository.GetById(id);
                if (donor == null)
                {
                    throw new UserNotFoundException("Donor not found");
                }

                donor.Age = age;
                var result = await _donorRepository.Update(donor);
                return MapDonorToDTO(result);
            }
            catch (Exception ex)
            {
                throw new DonorServiceException( ex);
            }
        }

        private ReturnDonorDTO MapDonorToDTO(Donor donor)
        {
            return new ReturnDonorDTO
            {
                DonorID = donor.DonorID,
                UserID = donor.UserID,
                Age = donor.Age
                // Map other properties as needed
            };
        }
    }
}
