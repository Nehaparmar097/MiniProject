using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Models;
using Job_Portal_API.Models.Enums;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_API.Tests.RepositoryTests
{
    public class BloodDonationRepositoryTests
    {
        private BloodDonationAppContext _context;
        private BloodDonationRepository _bloodDonationRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "BloodDonationApp")
                .Options;

            _context = new BloodDonationAppContext(options);
            _bloodDonationRepository = new BloodDonationRepository(_context);

            // Initialize Data
            var user = new User
            {
                UserID = 1,
                Email = "john@example.com",
                Password = new byte[] { 1, 2, 3 },
                HashKey = new byte[] { 4, 5, 6 },
                FirstName = "John",
                LastName = "Doe",
                ContactNumber = "1234567890",
                UserType = UserType.Donor
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var recipient = new Recipient
            {
                RecipientID = 1,
                UserID = 1,
                Age = 25,
                RequiredBloodType = "A+",
                BloodRequiredDate = DateTime.Now
            };

            _context.Recipients.Add(recipient);
            _context.SaveChanges();

            var donor = new Donor
            {
                DonorID = 1,
                UserID = 1,
                Age = 30
            };

            _context.Donors.Add(donor);
            _context.SaveChanges();

            var bloodStock = new BloodStock
            {
                ID = 1,
                BloodType = "A+",
                status = "Available",
                city = "CityName",
                state = "StateName",
                hospitalName = "HospitalName",
                donationDate = DateTime.Now,
                DonorID = 1
            };

            _context.BloodStocks.Add(bloodStock);
            _context.SaveChanges();

            var bloodDonation = new BloodDonation
            {
                BloodDonationID = 1,
                BloodStockID = 1,
                RecipientID = 1,
                DonationDate = DateTime.Now,
                BloodType = "A+"
            };

            _context.BloodDonations.Add(bloodDonation);
            _context.SaveChanges();
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
            var newBloodDonation = new BloodDonation
            {
                BloodDonationID = 2,
                BloodStockID = 1,
                RecipientID = 1,
                DonationDate = DateTime.Now,
                BloodType = "B+"
            };

            // Act
            var result = await _bloodDonationRepository.Add(newBloodDonation);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.BloodDonationID);
        }

        [Test]
        public async Task Update_Success()
        {
            // Arrange
            var bloodDonation = new BloodDonation
            {
                BloodDonationID = 1,
                BloodStockID = 1,
                RecipientID = 1,
                DonationDate = DateTime.Now,
                BloodType = "O+"
            };

            // Act
            var result = await _bloodDonationRepository.Update(bloodDonation);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("O+", result.BloodType);
        }

        [Test]
        public void Update_Fail_BloodDonationNotFound()
        {
            // Arrange
            var bloodDonation = new BloodDonation
            {
                BloodDonationID = 99, // Non-existing ID
                BloodStockID = 1,
                RecipientID = 1,
                DonationDate = DateTime.Now,
                BloodType = "AB+"
            };

            // Act & Assert
            Assert.ThrowsAsync<BloodDonationNotFoundException>(async () => await _bloodDonationRepository.Update(bloodDonation));
        }

        [Test]
        public async Task Delete_Success()
        {
            // Act
            var result = await _bloodDonationRepository.DeleteById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.BloodDonationID);
        }

        [Test]
        public void Delete_Fail_BloodDonationNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<BloodDonationNotFoundException>(async () => await _bloodDonationRepository.DeleteById(99)); // Non-existing ID
        }

        [Test]
        public async Task GetById_Success()
        {
            // Act
            var result = await _bloodDonationRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.BloodDonationID);
        }

        [Test]
        public void GetById_Fail_BloodDonationNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<BloodDonationNotFoundException>(async () => await _bloodDonationRepository.GetById(99)); // Non-existing ID
        }

        [Test]
        public async Task GetAll_Success()
        {
            // Act
            var result = await _bloodDonationRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAll_Fail()
        {
            // Arrange
            _context.BloodDonations.RemoveRange(_context.BloodDonations);
            await _context.SaveChangesAsync();

            // Act
            var result = await _bloodDonationRepository.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
