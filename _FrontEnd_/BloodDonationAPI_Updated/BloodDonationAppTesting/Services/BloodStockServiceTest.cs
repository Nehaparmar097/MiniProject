using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_API.Tests
{
    public class BloodStockServiceTests
    {
        private Mock<IRepository<int, BloodStock>> _mockBloodStockRepository;
        private BloodStockService _bloodStockService;

        [SetUp]
        public void Setup()
        {
            _mockBloodStockRepository = new Mock<IRepository<int, BloodStock>>();
            _bloodStockService = new BloodStockService(_mockBloodStockRepository.Object);
        }

        [Test]
        public async Task AddBloodStockAsync_Success()
        {
            // Arrange
            var bloodStockDto = new BloodStockDTO
            {
                BloodType = "A+",
                city = "New York",
                state = "NY",
                hospitalName = "NY Hospital",
                DonorID = 1
            };

            var newBloodStock = new BloodStock
            {
                ID = 1,
                BloodType = "A+",
                status = "available",
                city = "New York",
                state = "NY",
                hospitalName = "NY Hospital",
                donationDate = DateTime.Now,
                DonorID = 1
            };

            _mockBloodStockRepository.Setup(repo => repo.Add(It.IsAny<BloodStock>())).ReturnsAsync(newBloodStock);

            // Act
            var result = await _bloodStockService.AddBloodStockAsync(bloodStockDto);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(newBloodStock.ID, result.ID);
            Assert.AreEqual(newBloodStock.BloodType, result.BloodType);
            Assert.AreEqual(newBloodStock.status, result.status);
        }

        [Test]
        public void AddBloodStockAsync_Failure()
        {
            // Arrange
            var bloodStockDto = new BloodStockDTO
            {
                BloodType = "A+",
                city = "New York",
                state = "NY",
                hospitalName = "NY Hospital",
                DonorID = 1
            };

            _mockBloodStockRepository.Setup(repo => repo.Add(It.IsAny<BloodStock>())).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _bloodStockService.AddBloodStockAsync(bloodStockDto));
        }

        [Test]
        public async Task GetAllBloodStocksAsync_Success()
        {
            // Arrange
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", city = "New York", state = "NY", hospitalName = "NY Hospital", donationDate = DateTime.Now, DonorID = 1 },
                new BloodStock { ID = 2, BloodType = "B+", status = "unavailable", city = "Los Angeles", state = "CA", hospitalName = "LA Hospital", donationDate = DateTime.Now, DonorID = 2 }
            };

            _mockBloodStockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetAllBloodStocksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(bloodStocks.Count, result.Count);
        }

        [Test]
        public async Task GetBloodStockByIdAsync_Success()
        {
            // Arrange
            var bloodStock = new BloodStock
            {
                ID = 1,
                BloodType = "A+",
                status = "available",
                city = "New York",
                state = "NY",
                hospitalName = "NY Hospital",
                donationDate = DateTime.Now,
                DonorID = 1
            };

            _mockBloodStockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(bloodStock);

            // Act
            var result = await _bloodStockService.GetBloodStockByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(bloodStock.ID, result.ID);
            Assert.AreEqual(bloodStock.BloodType, result.BloodType);
            Assert.AreEqual(bloodStock.status, result.status);
        }

        [Test]
        public void GetBloodStockByIdAsync_NotFound()
        {
            // Arrange
            _mockBloodStockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ThrowsAsync(new BloodStockNotFoundException());

            // Act & Assert
            Assert.ThrowsAsync<BloodStockNotFoundException>(async () => await _bloodStockService.GetBloodStockByIdAsync(1));
        }

        [Test]
        public async Task GetBloodStocksByCityAsync_Success()
        {
            // Arrange
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", city = "New York", state = "NY", hospitalName = "NY Hospital", donationDate = DateTime.Now, DonorID = 1 },
                new BloodStock { ID = 2, BloodType = "A-", status = "unavailable", city = "New York", state = "NY", hospitalName = "NY Hospital", donationDate = DateTime.Now, DonorID = 2 }
            };

            _mockBloodStockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByCityAsync("New York");

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetBloodStocksByDonorIdAsync_Success()
        {
            // Arrange
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", city = "New York", state = "NY", hospitalName = "NY Hospital", donationDate = DateTime.Now, DonorID = 1 },
                new BloodStock { ID = 2, BloodType = "A-", status = "unavailable", city = "Los Angeles", state = "CA", hospitalName = "LA Hospital", donationDate = DateTime.Now, DonorID = 1 }
            };

            _mockBloodStockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByDonorIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetBloodStocksByHospitalAsync_Success()
        {
            // Arrange
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", city = "New York", state = "NY", hospitalName = "NY Hospital", donationDate = DateTime.Now, DonorID = 1 },
                new BloodStock { ID = 2, BloodType = "A-", status = "unavailable", city = "New York", state = "NY", hospitalName = "NY Hospital", donationDate = DateTime.Now, DonorID = 2 }
            };

            _mockBloodStockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByHospitalAsync("NY Hospital");

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetBloodStocksByStateAsync_Success()
        {
            // Arrange
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", city = "New York", state = "NY", hospitalName = "NY Hospital", donationDate = DateTime.Now, DonorID = 1 },
                new BloodStock { ID = 2, BloodType = "A-", status = "unavailable", city = "Buffalo", state = "NY", hospitalName = "Buffalo Hospital", donationDate = DateTime.Now, DonorID = 2 }
            };

            _mockBloodStockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByStateAsync("NY");

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

    }
}
       