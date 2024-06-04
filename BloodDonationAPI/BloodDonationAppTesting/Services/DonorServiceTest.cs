using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using BloodDonationApp.Exceptions;
using Assert = NUnit.Framework.Assert;

namespace Job_Portal_API.Tests.Services
{
    public class DonorServiceTest
    {
        private readonly Mock<IRepository<int, Donor>> _mockDonorRepository;
        private readonly DonorService _donorService;

        public DonorServiceTest()
        {
            _mockDonorRepository = new Mock<IRepository<int, Donor>>();
            _donorService = new DonorService(_mockDonorRepository.Object);
        }

        [Fact]
        public async Task AddDonorDetails_ShouldAddDonor()
        {
            // Arrange
            var donorDTO = new AddDonorDTO
            {
                UserID = 1,
                Age = 30
            };
            var donor = new Donor
            {
                DonorID = 1,
                UserID = donorDTO.UserID,
                Age = donorDTO.Age
            };
            _mockDonorRepository.Setup(r => r.Add(It.IsAny<Donor>())).ReturnsAsync(donor);

            // Act
            var result = await _donorService.AddDonorDetails(donorDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(donor.UserID, result.UserID);
            Assert.Equals(donor.Age, result.Age);
        }

        [Fact]
        public async Task AddDonorDetails_ShouldThrowDonorServiceException()
        {
            // Arrange
            var donorDTO = new AddDonorDTO
            {
                UserID = 1,
                Age = 30
            };
            _mockDonorRepository.Setup(r => r.Add(It.IsAny<Donor>())).ThrowsAsync(new Exception());

            // Act & Assert
             Assert.ThrowsAsync<DonorServiceException>(() => _donorService.AddDonorDetails(donorDTO));
        }

        [Fact]
        public async Task UpdateAge_ShouldUpdateDonorAge()
        {
            // Arrange
            var donor = new Donor
            {
                DonorID = 1,
                UserID = 1,
                Age = 30
            };
            var newAge = 35;
            _mockDonorRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(donor);
            _mockDonorRepository.Setup(r => r.Update(It.IsAny<Donor>())).ReturnsAsync(donor);

            // Act
            var result = await _donorService.UpdateAge(donor.DonorID, newAge);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(newAge, result.Age);
        }

        [Fact]
        public async Task UpdateAge_ShouldThrowUserNotFoundException()
        {
            // Arrange
            _mockDonorRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Donor)null);

            // Act & Assert
             Assert.ThrowsAsync<UserNotFoundException>(() => _donorService.UpdateAge(1, 35));
        }

        [Fact]
        public async Task UpdateAge_ShouldThrowDonorServiceException()
        {
            // Arrange
            var donor = new Donor
            {
                DonorID = 1,
                UserID = 1,
                Age = 30
            };
            _mockDonorRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(donor);
            _mockDonorRepository.Setup(r => r.Update(It.IsAny<Donor>())).ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<DonorServiceException>(() => _donorService.UpdateAge(1, 35));
        }
    }
}
