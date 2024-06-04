using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Services;
using Moq;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Job_Portal_API.Tests.Services
{
    public class BloodDonationServiceTest
    {
        private readonly Mock<IRepository<int, BloodDonation>> _mockBloodDonationRepository;
        private readonly Mock<IRepository<int, BloodStock>> _mockBloodStockRepository;
        private readonly Mock<IRepository<int, Recipient>> _mockRecipientRepository;
        private readonly BloodDonationService _bloodDonationService;

        public BloodDonationServiceTest()
        {
            _mockBloodDonationRepository = new Mock<IRepository<int, BloodDonation>>();
            _mockBloodStockRepository = new Mock<IRepository<int, BloodStock>>();
            _mockRecipientRepository = new Mock<IRepository<int, Recipient>>();
            _bloodDonationService = new BloodDonationService(
                _mockBloodDonationRepository.Object,
                _mockBloodStockRepository.Object,
                _mockRecipientRepository.Object);
        }

        [Fact]
        public async Task BloodDonatedTo_ShouldReturnBloodDonationResponseDTO()
        {
            // Arrange
            var donorId = 1;
            var bloodDonation = new BloodDonation
            {
                BloodDonationID = 1,
                RecipientID = 1,
                BloodStockID = 1,
                DonationDate = DateTime.Now,
                BloodType = "A+"
            };
            var bloodStock = new BloodStock { DonorID = donorId };
            _mockBloodDonationRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<BloodDonation> { bloodDonation });
            _mockBloodStockRepository.Setup(r => r.GetById(bloodDonation.BloodStockID)).ReturnsAsync(bloodStock);

            // Act
            var result = await _bloodDonationService.BloodDonatedTo(donorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(bloodDonation.BloodDonationID, result.BloodDonationID);
            Assert.Equals(bloodDonation.RecipientID, result.RecipientID);
            Assert.Equals(bloodDonation.BloodStockID, result.BloodStockID);
            Assert.Equals(bloodDonation.DonationDate, result.DonationDate);
            Assert.Equals(bloodDonation.BloodType, result.BloodType);
        }

        [Fact]
        public async Task BloodDonation_ShouldReturnBloodDonationResponseDTO()
        {
            // Arrange
            var bloodDonationRequestDTO = new BloodDonationRequestDTO
            {
                BloodStockID = 1,
                RecipientID = 1
            };
            var bloodStock = new BloodStock { ID = bloodDonationRequestDTO.BloodStockID, BloodType = "A+" };
            _mockBloodStockRepository.Setup(r => r.GetById(bloodDonationRequestDTO.BloodStockID)).ReturnsAsync(bloodStock);
            _mockBloodDonationRepository.Setup(r => r.Add(It.IsAny<BloodDonation>())).ReturnsAsync(new BloodDonation { BloodDonationID = 1 });

            // Act
            var result = await _bloodDonationService.BloodDonation(bloodDonationRequestDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(1, result.BloodDonationID);
            Assert.Equals(bloodDonationRequestDTO.RecipientID, result.RecipientID);
            Assert.Equals(bloodDonationRequestDTO.BloodStockID, result.BloodStockID);
            Assert.AreNotEqual(DateTime.MinValue, result.DonationDate);

            Assert.Equals(bloodStock.BloodType, result.BloodType);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfBloodDonationResponseDTO()
        {
            // Arrange
            var bloodDonations = new List<BloodDonation>
            {
                new BloodDonation { BloodDonationID = 1, RecipientID = 1, BloodStockID = 1, DonationDate = DateTime.Now, BloodType = "A+" },
                new BloodDonation { BloodDonationID = 2, RecipientID = 2, BloodStockID = 2, DonationDate = DateTime.Now, BloodType = "B+" }
            };
            _mockBloodDonationRepository.Setup(r => r.GetAll()).ReturnsAsync(bloodDonations);

            // Act
            var result = await _bloodDonationService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equals(bloodDonations.Count, result.Count);
        }
    }
}
