using BloodDonationApp.Exceptions;
using BloodDonationApp.Models.DTOs;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace JobPortalAPI.Tests
{
    [TestFixture]
    public class RecipientServiceTests
    {
        private Mock<IRepository<int, Recipient>> _mockRecipientRepository;
        private RecipientService _recipientService;

        [SetUp]
        public void Setup()
        {
            _mockRecipientRepository = new Mock<IRepository<int, Recipient>>();
            _recipientService = new RecipientService(_mockRecipientRepository.Object);
        }

        [Test]
        public async Task AddRecipientDetails_Success()
        {
            // Arrange
            var addRecipientDTO = new AddRecipientDTO
            {
                UserID = 1,
                Age = 30,
                RequiredBloodType = "O+",
                BloodRequiredDate = new DateTime(2024, 6, 15)
            };

            var recipient = new Recipient
            {
                RecipientID = 1,
                UserID = addRecipientDTO.UserID,
                Age = addRecipientDTO.Age,
                RequiredBloodType = addRecipientDTO.RequiredBloodType,
                BloodRequiredDate = addRecipientDTO.BloodRequiredDate
            };

            _mockRecipientRepository.Setup(repo => repo.Add(It.IsAny<Recipient>())).ReturnsAsync(recipient);

            // Act
            var result = await _recipientService.AddRecipientDetails(addRecipientDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(recipient.RecipientID, result.RecipientID);
            Assert.AreEqual(addRecipientDTO.UserID, result.UserID);
            Assert.AreEqual(addRecipientDTO.Age, result.Age);
            Assert.AreEqual(addRecipientDTO.RequiredBloodType, result.RequiredBloodType);
            Assert.AreEqual(addRecipientDTO.BloodRequiredDate, result.BloodRequiredDate);
        }

        [Test]
        public void AddRecipientDetails_Exception_Fail()
        {
            // Arrange
            var addRecipientDTO = new AddRecipientDTO
            {
                UserID = 1,
                Age = 30,
                RequiredBloodType = "O+",
                BloodRequiredDate = new DateTime(2024, 6, 15)
            };

            _mockRecipientRepository.Setup(repo => repo.Add(It.IsAny<Recipient>())).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<RecipientServiceException>(async () => await _recipientService.AddRecipientDetails(addRecipientDTO));
            Assert.IsInstanceOf<Exception>(ex.InnerException);
            Assert.AreEqual("Database error", ex.InnerException.Message);
        }

        [Test]
        public async Task UpdateAge_Success()
        {
            // Arrange
            var recipient = new Recipient
            {
                RecipientID = 1,
                UserID = 1,
                Age = 25,
                RequiredBloodType = "O+",
                BloodRequiredDate = DateTime.Now
            };

            _mockRecipientRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(recipient);
            _mockRecipientRepository.Setup(repo => repo.Update(It.IsAny<Recipient>())).ReturnsAsync((Recipient r) => r);

            // Act
            var result = await _recipientService.UpdateAge(recipient.RecipientID, 30);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(30, result.Age);
        }

        [Test]
        public void UpdateAge_RecipientNotFound_Fail()
        {
            // Arrange
            _mockRecipientRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Recipient)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<UserNotFoundException>(async () => await _recipientService.UpdateAge(1, 30));
            Assert.AreEqual("Recipient not found", ex.Message);
        }

        [Test]
        public async Task UpdateRequiredBloodType_Success()
        {
            // Arrange
            var recipient = new Recipient
            {
                RecipientID = 1,
                UserID = 1,
                Age = 25,
                RequiredBloodType = "A+",
                BloodRequiredDate = DateTime.Now
            };

            _mockRecipientRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(recipient);
            _mockRecipientRepository.Setup(repo => repo.Update(It.IsAny<Recipient>())).ReturnsAsync((Recipient r) => r);

            // Act
            var result = await _recipientService.UpdateRequiredBloodType(recipient.RecipientID, "B+");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("B+", result.RequiredBloodType);
        }

        [Test]
        public void UpdateRequiredBloodType_RecipientNotFound_Fail()
        {
            // Arrange
            _mockRecipientRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Recipient)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<UserNotFoundException>(async () => await _recipientService.UpdateRequiredBloodType(1, "B+"));
            Assert.AreEqual("Recipient not found", ex.Message);
        }

      
    }

}
