/*using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace Job_Portal_API.Services
{
    public class BloodStockService : IBloodStock
    {
        public Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByJobTypeAsync(string jobType)
        {
            throw new NotImplementedException();
        }

        private readonly IRepository<int, BloodStock> _bloodStockRepository;
        private readonly IRepository<int, Donor> _donorRepository;
        private string bloodtype;

        public BloodStockService(IRepository<int, BloodStock> bloodStockRepository, IRepository<int, Donor> donorRepository)
        {
            _bloodStockRepository = bloodStockRepository;
            _donorRepository = donorRepository;
        }

        public BloodStock MapDtoToBloodStock(BloodStockDTO bloodStockDto)
        {
            // Validate and convert BloodType
            if (!Enum.TryParse<Blood_Type>(bloodStockDto.BloodType, true, out var jobType))
            {
                throw new ArgumentException("Invalid Job type");
            }
            return new BloodStock
            {
                Disease_Title = bloodStockDto.Disease_Title,
                Disease_Description = bloodStockDto.Disease_Description,
                BloodType = bloodtype,
                Location = bloodStockDto.Location,
                Charge = bloodStockDto.Charge,
                donationDate = bloodStockDto.donationDate,
                expiryDate = bloodStockDto.expiryDate,
                DonorID = bloodStockDto.DonorID,
                DonorBloods = bloodStockDto.BloodType.Select(bloodtype => new DonorBloodDTO { bloodtype = bloodtype.bloodtype }).ToList()
            };
        }
        public BloodStockResponseDTO MapBloodStockToDto(BloodStock bloodStock)
        {

            var result = new BloodStockResponseDTO
            {
                ID = bloodStock.ID,
                Disease_Title = bloodStock.Disease_Title,
                Disease_Description = bloodStock.Disease_Description,
                BloodType = bloodStock.BloodType.ToString(),
                Location = bloodStock.Location,
                Charge = bloodStock.Charge,
                donationDate = bloodStock.donationDate,
                expiryDate = bloodStock.expiryDate,
                DonorID = bloodStock.DonorID,
                DonorBloodType = bloodStock.DonorBloods.Select(bloodtype => new DonorBloodDTO { bloodtype = bloodtype.bloodtype }).ToList()
            };
            return result;
        }
        public async Task<BloodStockResponseDTO> AddBloodStockAsync(BloodStockDTO bloodStockDto)
        {
            try
            {
                var donor = await _donorRepository.GetById(bloodStockDto.DonorID);
                var bloodStock = MapDtoToBloodStock(bloodStockDto);

                var result = await _bloodStockRepository.Add(bloodStock);
                var response = MapBloodStockToDto(result);
                return response;

            }
            catch (UserNotFoundException e)
            {
                throw new UserNotFoundException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public async Task<IEnumerable<BloodStockResponseDTO>> GetAllBloodStocksAsync()
        {
            var bloodStocks = await _bloodStockRepository.GetAll();
            if (bloodStocks.Count() == 0)
            {
                throw new NoJobExistException("No Job Listings Exist");
            }

            return bloodStocks.Select(bloodStock => new BloodStockResponseDTO
            {
                ID = bloodStock.ID,
                Disease_Title = bloodStock.Disease_Title,
                Disease_Description = bloodStock.Disease_Description,
                BloodType = bloodStock.BloodType.ToString(),
                Location = bloodStock.Location,
                Charge = bloodStock.Charge,
                donationDate = bloodStock.donationDate,
                expiryDate = bloodStock.expiryDate,
                DonorID = bloodStock.DonorID,
                DonorBloodType = bloodStock.DonorBloods.Select(bloodtype => new DonorBloodDTO { bloodtype = bloodtype.bloodtype }).ToList()
            });
        }

        public async Task<BloodStockResponseDTO> GetBloodStockByIdAsync(int id)
        {
            try
            {
                var bloodStock = await _bloodStockRepository.GetById(id);
                return new BloodStockResponseDTO
                {
                    ID = bloodStock.ID,
                    Disease_Title = bloodStock.Disease_Title,
                    Disease_Description = bloodStock.Disease_Description,
                    BloodType = bloodStock.BloodType.ToString(),
                    Location = bloodStock.Location,
                    Charge = bloodStock.Charge,
                    donationDate = bloodStock.donationDate,
                    expiryDate = bloodStock.expiryDate,
                    DonorID = bloodStock.DonorID,
                    DonorBloodType = bloodStock.DonorBloods.Select(bloodtype => new DonorBloodDTO { bloodtype = bloodtype.bloodtype }).ToList()
                };
            }
            catch (BloodStockNotFoundException e)
            {
                throw new BloodStockNotFoundException(e.Message);
            }



        }

        public async Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByDonorIdAsync(int id)
        {
            try
            {
                var donorExists = await _donorRepository.GetById(id);
                var bloodStocks = await _bloodStockRepository.GetAll();
                if (bloodStocks.Count() == 0)
                {
                    throw new NoJobExistException("No Job Listings Exist");
                }
                var donorBloodStocks = bloodStocks.Where(jl => jl.DonorID == id);
                return donorBloodStocks.Select(bloodStock => new BloodStockResponseDTO
                {
                    ID = bloodStock.ID,
                    Disease_Title = bloodStock.Disease_Title,
                    Disease_Description = bloodStock.Disease_Description,
                    BloodType = bloodStock.BloodType.ToString(),
                    Location = bloodStock.Location,
                    Charge = bloodStock.Charge,
                    donationDate = bloodStock.donationDate,
                    expiryDate = bloodStock.expiryDate,
                    DonorID = bloodStock.DonorID,
                    DonorBloodType = bloodStock.DonorBloods.Select(bloodtype => new DonorBloodDTO { bloodtype = bloodtype.bloodtype }).ToList()
                });
            }
            catch (UserNotFoundException e)
            {
                throw new UserNotFoundException(e.Message);
            }
            catch (NoJobExistException e)
            {
                throw new NoJobExistException(e.Message);
            }

        }

        public async Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByLocationAsync(string location)
        {
            try
            {
                var bloodStocks = await GetAllBloodStocksAsync();

                var filteredBloodStocks = bloodStocks
                .Where(job => string.Equals(job.Location.Trim(), location.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                if (filteredBloodStocks.Count == 0)
                {
                    throw new BloodStockNotFoundException("No Job Listings Found");
                }

                return filteredBloodStocks;
            }
            catch (BloodStockNotFoundException e)
            {
                throw new BloodStockNotFoundException(e.Message);
            }
        }

        public async Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByChargeAsync(double sRange, double eRange)
        {
            try
            {
                var bloodStocks = await GetAllBloodStocksAsync();

                var filteredBloodStocks = bloodStocks
                .Where(job => job.Charge >= sRange && job.Charge <= eRange)
                .ToList();

                // Check if any job listings are found within the specified salary range
                if (!filteredBloodStocks.Any())
                {
                    throw new BloodStockNotFoundException($"No Job Listings Found within the salary range: {sRange} - {eRange}");
                }
                return filteredBloodStocks;


            }
            catch (BloodStockNotFoundException e)
            {
                throw new BloodStockNotFoundException(e.Message);
            }
        }

        public async Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByTitleAsync(string title)
        {
            try
            {
                var bloodStocks = await GetAllBloodStocksAsync();
                var filteredBloodStocks = bloodStocks
                .Where(job => string.Equals(job.Disease_Title.Trim(), title.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                if (!filteredBloodStocks.Any())
                {
                    throw new BloodStockNotFoundException($"No blood stock Found for the Given Disease_Title {title}");
                }
                return filteredBloodStocks;

            }
            catch (BloodStockNotFoundException e)
            {
                throw new BloodStockNotFoundException(e.Message);
            }
        }

        public async Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByBloodTypeAsync(string jobType)
        {    // Validate and convert BloodType
            if (!Enum.TryParse<Blood_Type>(jobType, true, out var newBloodType))
            {
                throw new InvalidBloodTypeException("invalid blood type");
            }
            try
            {
                var bloodStocks = await GetAllBloodStocksAsync();
                var filteredBloodStocks = bloodStocks
                    .Where(job => string.Equals(job.BloodType.ToString().Trim(), jobType.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                if (filteredBloodStocks.Count == 0)
                {
                    throw new BloodStockNotFoundException($"No blood stock Found for the Given BloodType {jobType}");
                }
                return filteredBloodStocks;
            }
            catch (BloodStockNotFoundException e)
            {
                throw new BloodStockNotFoundException(e.Message);
            }
        }
    }
}*/
using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions
{
    [Serializable]
    internal class NoBloodStockExistException : Exception
    {
        public NoBloodStockExistException()
        {
        }

        public NoBloodStockExistException(string? message) : base(message)
        {
        }

        public NoBloodStockExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoBloodStockExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}