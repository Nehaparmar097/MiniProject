using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Models;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_API.Tests.RepositoryTests
{
    public class RecipientRepositoryTests
    {
        private BloodDonationAppContext _context;
        private RecipientRepository _recipientRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "RecipientRepositoryTestDb")
                .Options;

            _context = new BloodDonationAppContext(options);
            _recipientRepository = new RecipientRepository(_context);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Add_Success()
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

            // Act
            var result = await _recipientRepository.Add(recipient);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.RecipientID);
        }

        [Test]
        public async Task Update_Success()
        {
            // Arrange
            var recipient = new Recipient
            {
                RecipientID = 1,
                UserID = 1,
                Age = 25,
                RequiredBloodType = "B+",
                BloodRequiredDate = DateTime.Now
            };

            // Act
            var result = await _recipientRepository.Update(recipient);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("B+", result.RequiredBloodType);
        }

        [Test]
        public void Update_Fail_RecipientNotFound()
        {
            // Arrange
            var recipient = new Recipient
            {
                RecipientID = 99, // Non-existing ID
                UserID = 1,
                Age = 25,
                RequiredBloodType = "B+",
                BloodRequiredDate = DateTime.Now
            };

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _recipientRepository.Update(recipient));
        }

        [Test]
        public async Task Delete_Success()
        {
            // Act
            var result = await _recipientRepository.DeleteById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.RecipientID);
        }

        [Test]
        public void Delete_Fail_RecipientNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _recipientRepository.DeleteById(99)); // Non-existing ID
        }

        [Test]
        public async Task GetById_Success()
        {
            // Act
            var result = await _recipientRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.RecipientID);
        }

        [Test]
        public void GetById_Fail_RecipientNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<RecipientNotFoundException>(async () => await _recipientRepository.GetById(99)); // Non-existing ID
        }

    }
}


