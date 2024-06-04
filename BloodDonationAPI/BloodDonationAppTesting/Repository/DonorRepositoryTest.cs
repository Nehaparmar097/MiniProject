using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Models;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalAPITest.RepositoryTests
{
    public class DonorRepositoryTest
    {
        private BloodDonationAppContext _context;
        private DonorRepository _donorRepository;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "testDB")
                .Options;
            _context = new BloodDonationAppContext(options);
            _donorRepository = new DonorRepository(_context);
        }

        [Test]
        public async Task AddDonor_Success()
        {
            // Arrange
            var newDonor = new Donor
            {
                UserID = 1,
                Age = 30,
                User = new User { Email = "newdonor@example.com" }
            };

            // Act
            var result = await _donorRepository.Add(newDonor);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(newDonor.UserID, result.UserID);
        }

        [Test]
        public void AddDonor_Failure_DonorAlreadyExists()
        {
            // Arrange
            var existingDonor = new Donor
            {
                UserID = 1,
                Age = 30,
                User = new User { Email = "existingdonor@example.com" }
            };

            // Seed the database with the existing donor
            _context.Donors.Add(existingDonor);
            _context.SaveChanges();

            var newDonor = new Donor
            {
                UserID = 1, // Same UserID as existing donor
                Age = 25,
                User = new User { Email = "newdonor@example.com" }
            };

            // Act & Assert
            Assert.ThrowsAsync<UserAlreadyExistException>(async () => await _donorRepository.Add(newDonor));
        }

        [Test]
        public async Task UpdateDonor_Success()
        {
            // Arrange
            var existingDonor = new Donor
            {
                UserID = 1,
                Age = 30,
                User = new User { Email = "donor@example.com" }
            };

            var addedDonor = await _donorRepository.Add(existingDonor);
            //  addedDonor.User.Name = "Updated Donor";

            // Act
            var result = await _donorRepository.Update(addedDonor);

            // Assert
            Assert.NotNull(result);
            //Assert.AreEqual("Updated Donor");
        }

        [Test]
        public void UpdateDonor_Failure_DonorNotFound()
        {
            // Arrange
            var nonExistentDonor = new Donor
            {
                DonorID = 999, 
                UserID = 999,
                Age = 30,
                User = new User { Email = "nonexistentdonor@example.com" }
            };

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _donorRepository.Update(nonExistentDonor));
        }

        [Test]
        public async Task DeleteDonorById_Success()
        {
            // Arrange
            var existingDonor = new Donor
            {
                UserID = 1,
                Age = 30,
                User = new User { Email = "deletedonor@example.com" }
            };

            var addedDonor = await _donorRepository.Add(existingDonor);

            // Act
            var result = await _donorRepository.DeleteById(addedDonor.DonorID);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(addedDonor.DonorID, result.DonorID);
        }

        [Test]
        public void DeleteDonorById_Failure_DonorNotFound()
        {
            // Arrange
            var nonExistentDonorId = 999; // Assuming this donor ID does not exist

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _donorRepository.DeleteById(nonExistentDonorId));
        }

        [Test]
        public async Task GetDonorById_Success()
        {
            // Arrange
            var existingDonor = new Donor
            {
                UserID = 1,
                Age = 30,
                User = new User { Email = "getdonor@example.com" }
            };

            var addedDonor = await _donorRepository.Add(existingDonor);

            // Act
            var result = await _donorRepository.GetById(addedDonor.DonorID);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(addedDonor.DonorID, result.DonorID);
        }

        [Test]
        public void GetDonorById_Failure_DonorNotFound()
        {
            // Arrange
            var nonExistentDonorId = 999; // Assuming this donor ID does not exist

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _donorRepository.GetById(nonExistentDonorId));
        }

        [Test]
        public async Task GetAllDonors_Success()
        {
            // Arrange
            var donor1 = new Donor
            {
                UserID = 1,
                Age = 30,
                User = new User { Email = "donor1@example.com" }
            };
            var donor2 = new Donor
            {
                UserID = 2,
                Age = 25,
                User = new User { Email = "donor2@example.com" }
            };

            await _donorRepository.Add(donor1);
            await _donorRepository.Add(donor2);

            // Act
            var result = await _donorRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAllDonors_Failure_NoDonorsPresent()
        {
            // Act
            var result = await _donorRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count());
        }
    }
}