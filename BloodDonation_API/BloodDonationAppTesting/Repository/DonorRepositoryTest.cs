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
    public class DonorRepositoryTest
    {
        private BloodDonationAppContext _context;
        private DonorRepository _donorRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "DonorRepositoryTestDb")
                .Options;

            _context = new BloodDonationAppContext(options);
            _donorRepository = new DonorRepository(_context);
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
            var donor = new Donor
            {
                DonorID = 1,
                UserID = 1,
                Age = 30
            };

            // Act
            var result = await _donorRepository.Add(donor);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.DonorID);
        }

        [Test]
        public async Task Add_Fail_UserAlreadyExist()
        {
            // Arrange
            var donor = new Donor
            {
                DonorID = 1,
                UserID = 1,
                Age = 30
            };

            // Act & Assert
            Assert.ThrowsAsync<UserAlreadyExistException>(async () => await _donorRepository.Add(donor));
        }

        [Test]
        public async Task Update_Success()
        {
            // Arrange
            var donor = new Donor
            {
                DonorID = 1,
                UserID = 1,
                Age = 35 // updated age
            };

            // Act
            var result = await _donorRepository.Update(donor);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(35, result.Age);
        }

        [Test]
        public void Update_Fail_UserNotFound()
        {
            // Arrange
            var donor = new Donor
            {
                DonorID = 99, // Non-existing ID
                UserID = 1,
                Age = 35
            };

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _donorRepository.Update(donor));
        }

        [Test]
        public async Task Delete_Success()
        {
            // Act
            var result = await _donorRepository.DeleteById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.DonorID);
        }

        [Test]
        public void Delete_Fail_UserNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _donorRepository.DeleteById(99)); // Non-existing ID
        }

        [Test]
        public async Task GetById_Success()
        {
            // Act
            var result = await _donorRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.DonorID);
        }

        [Test]
        public void GetById_Fail_UserNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _donorRepository.GetById(99)); // Non-existing ID
        }

        [Test]
        public async Task GetAll_Success()
        {
            // Act
            var result = await _donorRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAll_Empty()
        {
            // Arrange
            _context.Donors.RemoveRange(_context.Donors);
            await _context.SaveChangesAsync();

            // Act
            var result = await _donorRepository.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
