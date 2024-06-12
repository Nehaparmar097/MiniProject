using BloodDonationApp.Exceptions;
using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Job_Portal_API.Tests
{
    public class DonorServiceTest
    {
        private Mock<IRepository<int, Donor>> _mockDonorRepository;
        private DonorService _donorService;

        [SetUp]
        public void Setup()
        {
            _mockDonorRepository = new Mock<IRepository<int, Donor>>();
            _donorService = new DonorService(_mockDonorRepository.Object);
        }

        [Test]
        public async Task AddDonorDetails_Success()
        {
            // Arrange
            var newDonorDTO = new AddDonorDTO
            {
                UserID = 1,
                Age = 30
            };

            var newDonor = new Donor
            {
                DonorID = 1,
                UserID = 1,
                Age = 30
            };

            _mockDonorRepository.Setup(repo => repo.Add(It.IsAny<Donor>())).ReturnsAsync(newDonor);

            // Act
            var result = await _donorService.AddDonorDetails(newDonorDTO);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(newDonor.DonorID, result.DonorID);
            Assert.AreEqual(newDonor.UserID, result.UserID);
            Assert.AreEqual(newDonor.Age, result.Age);
        }

        [Test]
        public void AddDonorDetails_Failure()
        {
            // Arrange
            var newDonorDTO = new AddDonorDTO
            {
                UserID = 1,
                Age = 30
            };

            _mockDonorRepository.Setup(repo => repo.Add(It.IsAny<Donor>())).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            Assert.ThrowsAsync<DonorServiceException>(async () => await _donorService.AddDonorDetails(newDonorDTO));
        }

        [Test]
        public async Task UpdateAge_Success()
        {
            // Arrange
            var donorID = 1;
            var updatedAge = 35;

            var existingDonor = new Donor
            {
                DonorID = donorID,
                UserID = 1,
                Age = 30
            };

            var updatedDonor = new Donor
            {
                DonorID = donorID,
                UserID = 1,
                Age = updatedAge
            };

            _mockDonorRepository.Setup(repo => repo.GetById(donorID)).ReturnsAsync(existingDonor);
            _mockDonorRepository.Setup(repo => repo.Update(It.IsAny<Donor>())).ReturnsAsync(updatedDonor);

            // Act
            var result = await _donorService.UpdateAge(donorID, updatedAge);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(updatedDonor.DonorID, result.DonorID);
            Assert.AreEqual(updatedDonor.UserID, result.UserID);
            Assert.AreEqual(updatedDonor.Age, result.Age);
        }

        [Test]
        public void UpdateAge_DonorNotFound()
        {
            // Arrange
            var donorID = 1;
            var updatedAge = 35;

            _mockDonorRepository.Setup(repo => repo.GetById(donorID)).ThrowsAsync(new UserNotFoundException("Donor not found"));

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _donorService.UpdateAge(donorID, updatedAge));
        }

        [Test]
        public void UpdateAge_Failure()
        {
            // Arrange
            var donorID = 1;
            var updatedAge = 35;

            var existingDonor = new Donor
            {
                DonorID = donorID,
                UserID = 1,
                Age = 30
            };

            _mockDonorRepository.Setup(repo => repo.GetById(donorID)).ReturnsAsync(existingDonor);
            _mockDonorRepository.Setup(repo => repo.Update(It.IsAny<Donor>())).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            Assert.ThrowsAsync<DonorServiceException>(async () => await _donorService.UpdateAge(donorID, updatedAge));
        }
    }
}
