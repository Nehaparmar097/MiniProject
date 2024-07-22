using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Models;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Job_Portal_API.Tests.RepositoryTests
{
    public class BloodStockRepositoryTest
    {
        private BloodDonationAppContext _context;
        private BloodStockRepository _bloodStockRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "BloodStockRepositoryTestDb")
                .Options;

            _context = new BloodDonationAppContext(options);
            _bloodStockRepository = new BloodStockRepository(_context);

            // Initialize Data
            var donor = new Donor
            {
                DonorID = 1
            };

            _context.Donors.Add(donor);
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
            var newBloodStock = new BloodStock
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

            // Act
            var result = await _bloodStockRepository.Add(newBloodStock);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [Test]
        public async Task Update_Success()
        {
            // Arrange
            var bloodStock = new BloodStock
            {
                ID = 1,
                BloodType = "B+",
                status = "Available",
                city = "CityName",
                state = "StateName",
                hospitalName = "HospitalName",
                donationDate = DateTime.Now,
                DonorID = 1
            };

            // Act
            var result = await _bloodStockRepository.Update(bloodStock);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("B+", result.BloodType);
        }

        [Test]
        public void Update_Fail_BloodStockNotFound()
        {
            // Arrange
            var bloodStock = new BloodStock
            {
                ID = 99, // Non-existing ID
                BloodType = "AB+",
                status = "Available",
                city = "CityName",
                state = "StateName",
                hospitalName = "HospitalName",
                donationDate = DateTime.Now,
                DonorID = 1
            };

            // Act & Assert
            Assert.ThrowsAsync<BloodStockNotFoundException>(async () => await _bloodStockRepository.Update(bloodStock));
        }

        [Test]
        public async Task Delete_Success()
        {
            // Arrange
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

            // Act
            var result = await _bloodStockRepository.DeleteById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [Test]
        public void Delete_Fail_BloodStockNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<BloodStockNotFoundException>(async () => await _bloodStockRepository.DeleteById(99)); // Non-existing ID
        }

        [Test]
        public async Task GetById_Success()
        {
            // Act
            var result = await _bloodStockRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [Test]
        public void GetById_Fail_BloodStockNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<BloodStockNotFoundException>(async () => await _bloodStockRepository.GetById(99)); // Non-existing ID
        }

        [Test]
        public async Task GetAll_Success()
        {
            // Act
            var result = await _bloodStockRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAll_Fail()
        {
            // Arrange
            _context.BloodStocks.RemoveRange(_context.BloodStocks);
            await _context.SaveChangesAsync();

            // Act
            var result = await _bloodStockRepository.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
