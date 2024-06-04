using System;
using System.Threading.Tasks;
using BloodDonationApp.Exceptions;
using BloodDonationApp.Models.DTOs;
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
    public class RecipientServiceTest
    {
        private readonly Mock<IRepository<int, Recipient>> _mockRecipientRepository;
        private readonly RecipientService _recipientService;

        public RecipientServiceTest()
        {
            _mockRecipientRepository = new Mock<IRepository<int, Recipient>>();
            _recipientService = new RecipientService(_mockRecipientRepository.Object);
        }

        [Fact]
        public async Task AddRecipientDetails_ShouldAddRecipient()
        {
            // Arrange
            var recipientDTO = new AddRecipientDTO
            {
                UserID = 1,
                Age = 25,
                RequiredBloodType = "A+",
                BloodRequiredDate = DateTime.Now
            };
            var recipient = new Recipient
            {
                RecipientID = 1,
                UserID = recipientDTO.UserID,
                Age = recipientDTO.Age,
                RequiredBloodType = recipientDTO.RequiredBloodType,
                BloodRequiredDate = recipientDTO.BloodRequiredDate
            };
            _mockRecipientRepository.Setup(r => r.Add(It.IsAny<Recipient>())).ReturnsAsync(recipient);

            // Act
            var result = await _recipientService.AddRecipientDetails(recipientDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(recipient.UserID, result.UserID);
            Assert.Equals(recipient.Age, result.Age);
            Assert.Equals(recipient.RequiredBloodType, result.RequiredBloodType);
            Assert.Equals(recipient.BloodRequiredDate, result.BloodRequiredDate);
        }

        [Fact]
        public async Task AddRecipientDetails_ShouldThrowRecipientServiceException()
        {
            // Arrange
            var recipientDTO = new AddRecipientDTO
            {
                UserID = 1,
                Age = 25,
                RequiredBloodType = "A+",
                BloodRequiredDate = DateTime.Now
            };
            _mockRecipientRepository.Setup(r => r.Add(It.IsAny<Recipient>())).ThrowsAsync(new Exception());

            // Act & Assert
             Assert.ThrowsAsync<RecipientServiceException>(() => _recipientService.AddRecipientDetails(recipientDTO));
        }

        [Fact]
        public async Task UpdateAge_ShouldUpdateRecipientAge()
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
            var newAge = 30;
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(recipient);
            _mockRecipientRepository.Setup(r => r.Update(It.IsAny<Recipient>())).ReturnsAsync(recipient);

            // Act
            var result = await _recipientService.UpdateAge(recipient.RecipientID, newAge);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(newAge, result.Age);
        }

        [Fact]
        public async Task UpdateAge_ShouldThrowUserNotFoundException()
        {
            // Arrange
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Recipient)null);

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(() => _recipientService.UpdateAge(1, 30));
        }

        [Fact]
        public async Task UpdateAge_ShouldThrowRecipientServiceException()
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
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(recipient);
            _mockRecipientRepository.Setup(r => r.Update(It.IsAny<Recipient>())).ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<RecipientServiceException>(() => _recipientService.UpdateAge(1, 30));
        }

        [Fact]
        public async Task UpdateRequiredBloodType_ShouldUpdateRequiredBloodType()
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
            var newBloodType = "B+";
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(recipient);
            _mockRecipientRepository.Setup(r => r.Update(It.IsAny<Recipient>())).ReturnsAsync(recipient);

            // Act
            var result = await _recipientService.UpdateRequiredBloodType(recipient.RecipientID, newBloodType);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(newBloodType, result.RequiredBloodType);
        }

        [Fact]
        public async Task UpdateRequiredBloodType_ShouldThrowUserNotFoundException()
        {
            // Arrange
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Recipient)null);

            // Act & Assert
             Assert.ThrowsAsync<UserNotFoundException>(() => _recipientService.UpdateRequiredBloodType(1, "B+"));
        }

        [Fact]
        public async Task UpdateRequiredBloodType_ShouldThrowRecipientServiceException()
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
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(recipient);
            _mockRecipientRepository.Setup(r => r.Update(It.IsAny<Recipient>())).ThrowsAsync(new Exception());

            // Act & Assert
             Assert.ThrowsAsync<RecipientServiceException>(() => _recipientService.UpdateRequiredBloodType(1, "B+"));
        }

        [Fact]
        public async Task UpdateBloodRequiredDate_ShouldUpdateBloodRequiredDate()
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
            var newDate = DateTime.Now.AddDays(10);
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(recipient);
            _mockRecipientRepository.Setup(r => r.Update(It.IsAny<Recipient>())).ReturnsAsync(recipient);

            // Act
            var result = await _recipientService.UpdateBloodRequiredDate(recipient.RecipientID, newDate);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(newDate, result.BloodRequiredDate);
        }

        [Fact]
        public async Task UpdateBloodRequiredDate_ShouldThrowUserNotFoundException()
        {
            // Arrange
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Recipient)null);

            // Act & Assert
             Assert.ThrowsAsync<UserNotFoundException>(() => _recipientService.UpdateBloodRequiredDate(1, DateTime.Now.AddDays(10)));
        }

        [Fact]
        public async Task UpdateBloodRequiredDate_ShouldThrowRecipientServiceException()
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
            _mockRecipientRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(recipient);
            _mockRecipientRepository.Setup(r => r.Update(It.IsAny<Recipient>())).ThrowsAsync(new Exception());

            // Act & Assert
             Assert.ThrowsAsync<RecipientServiceException>(() => _recipientService.UpdateBloodRequiredDate(1, DateTime.Now.AddDays(10)));
        }
    }
}
