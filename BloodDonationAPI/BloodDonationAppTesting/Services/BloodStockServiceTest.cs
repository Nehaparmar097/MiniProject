using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BloodStockServiceTest
    {
        private readonly Mock<IRepository<int, BloodStock>> _mockBloodStockRepository;
        private readonly BloodStockService _bloodStockService;

        public BloodStockServiceTest()
        {
            _mockBloodStockRepository = new Mock<IRepository<int, BloodStock>>();
            _bloodStockService = new BloodStockService(_mockBloodStockRepository.Object);
        }

        [Fact]
        public async Task AddBloodStockAsync_ShouldAddBloodStock()
        {
            // Arrange
            var bloodStockDTO = new BloodStockDTO
            {
                BloodType = "A+",
                city = "City1",
                state = "State1",
                hospitalName = "Hospital1",
                DonorID = 1
            };
            var bloodStock = new BloodStock
            {
                ID = 1,
                BloodType = bloodStockDTO.BloodType,
                status = "available",
                city = bloodStockDTO.city,
                state = bloodStockDTO.state,
                hospitalName = bloodStockDTO.hospitalName,
                donationDate = DateTime.Now,
                DonorID = bloodStockDTO.DonorID
            };
            _mockBloodStockRepository.Setup(r => r.Add(It.IsAny<BloodStock>())).ReturnsAsync(bloodStock);

            // Act
            var result = await _bloodStockService.AddBloodStockAsync(bloodStockDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(bloodStock.ID, result.ID);
            Assert.Equals(bloodStock.BloodType, result.BloodType);
            Assert.Equals(bloodStock.status, result.status);
        }

        [Fact]
        public async Task AddBloodStockAsync_ShouldThrowException()
        {
            // Arrange
            var bloodStockDTO = new BloodStockDTO
            {
                BloodType = "A+",
                city = "City1",
                state = "State1",
                hospitalName = "Hospital1",
                DonorID = 1
            };
            _mockBloodStockRepository.Setup(r => r.Add(It.IsAny<BloodStock>())).ThrowsAsync(new Exception());

            // Act & Assert
             Assert.ThrowsAsync<Exception>(() => _bloodStockService.AddBloodStockAsync(bloodStockDTO));
        }

        [Fact]
        public async Task GetAllBloodStocksAsync_ShouldReturnBloodStocks()
        {
            // Arrange
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available" },
                new BloodStock { ID = 2, BloodType = "B+", status = "unavailable" }
            };
            _mockBloodStockRepository.Setup(r => r.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetAllBloodStocksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equals(bloodStocks.Count, result.Count);
        }

        [Fact]
        public async Task GetAllBloodStocksAsync_ShouldThrowException()
        {
            // Arrange
            _mockBloodStockRepository.Setup(r => r.GetAll()).ThrowsAsync(new Exception());

            // Act & Assert
             Assert.ThrowsAsync<Exception>(() => _bloodStockService.GetAllBloodStocksAsync());
        }

        [Fact]
        public async Task GetBloodStockByIdAsync_ShouldReturnBloodStock()
        {
            // Arrange
            var bloodStock = new BloodStock { ID = 1, BloodType = "A+", status = "available" };
            _mockBloodStockRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(bloodStock);

            // Act
            var result = await _bloodStockService.GetBloodStockByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(bloodStock.ID, result.ID);
            Assert.Equals(bloodStock.BloodType, result.BloodType);
            Assert.Equals(bloodStock.status, result.status);
        }

        [Fact]
        public async Task GetBloodStockByIdAsync_ShouldThrowBloodStockNotFoundException()
        {
            // Arrange
            _mockBloodStockRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((BloodStock)null);

            // Act & Assert
             Assert.ThrowsAsync<BloodStockNotFoundException>(() => _bloodStockService.GetBloodStockByIdAsync(1));
        }

        [Fact]
        public async Task GetBloodStocksByCityAsync_ShouldReturnBloodStocks()
        {
            // Arrange
            var city = "City1";
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", city = city },
                new BloodStock { ID = 2, BloodType = "B+", status = "unavailable", city = city }
            };
            _mockBloodStockRepository.Setup(r => r.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByCityAsync(city);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(2, result.Count);
        }

        [Fact]
        public async Task GetBloodStocksByCityAsync_ShouldThrowException()
        {
            // Arrange
            var city = "City1";
            _mockBloodStockRepository.Setup(r => r.GetAll()).ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _bloodStockService.GetBloodStocksByCityAsync(city));
        }

        [Fact]
        public async Task GetBloodStocksByDonorIdAsync_ShouldReturnBloodStocks()
        {
            // Arrange
            var donorId = 1;
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", DonorID = donorId },
                new BloodStock { ID = 2, BloodType = "B+", status = "unavailable", DonorID = donorId }
            };
            _mockBloodStockRepository.Setup(r => r.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByDonorIdAsync(donorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(2, result.Count);
        }

        [Fact]
        public async Task GetBloodStocksByDonorIdAsync_ShouldThrowException()
        {
            // Arrange
            var donorId = 1;
            _mockBloodStockRepository.Setup(r => r.GetAll()).ThrowsAsync(new Exception());

            // Act & Assert
             Assert.ThrowsAsync<Exception>(() => _bloodStockService.GetBloodStocksByDonorIdAsync(donorId));
        }

        [Fact]
        public async Task GetBloodStocksByHospitalAsync_ShouldReturnBloodStocks()
        {
            // Arrange
            var hospitalName = "Hospital1";
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", hospitalName = hospitalName },
                new BloodStock { ID = 2, BloodType = "B+", status = "unavailable", hospitalName = hospitalName }
            };
            _mockBloodStockRepository.Setup(r => r.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByHospitalAsync(hospitalName);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(2, result.Count);
        }

        [Fact]
        public async Task GetBloodStocksByHospitalAsync_ShouldThrowException()
        {
            // Arrange
            var hospitalName = "Hospital1";
            _mockBloodStockRepository.Setup(r => r.GetAll()).ThrowsAsync(new Exception());

            // Act & Assert
             Assert.ThrowsAsync<Exception>(() => _bloodStockService.GetBloodStocksByHospitalAsync(hospitalName));
        }

        [Fact]
        public async Task GetBloodStocksByStateAsync_ShouldReturnBloodStocks()
        {
            // Arrange
            var state = "State1";
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available", state = state },
                new BloodStock { ID = 2, BloodType = "B+", status = "unavailable", state = state }
            };
            _mockBloodStockRepository.Setup(r => r.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByStateAsync(state);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(2, result.Count);
        }

        [Fact]
        public async Task GetBloodStocksByStateAsync_ShouldThrowException()
        {
            // Arrange
            var state = "State1";
            _mockBloodStockRepository.Setup(r => r.GetAll()).ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _bloodStockService.GetBloodStocksByStateAsync(state));
        }

        [Fact]
        public async Task GetBloodStocksByAvailableAsync_ShouldReturnAvailableBloodStocks()
        {
            // Arrange
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock { ID = 1, BloodType = "A+", status = "available" },
                new BloodStock { ID = 2, BloodType = "B+", status = "unavailable" },
                new BloodStock { ID = 3, BloodType = "O+", status = "available" }
            };
            _mockBloodStockRepository.Setup(r => r.GetAll()).ReturnsAsync(bloodStocks);

            // Act
            var result = await _bloodStockService.GetBloodStocksByAvailableAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equals(2, result.Count);
        }

        [Fact]
        public async Task GetBloodStocksByAvailableAsync_ShouldThrowException()
        {
            // Arrange
            _mockBloodStockRepository.Setup(r => r.GetAll()).ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _bloodStockService.GetBloodStocksByAvailableAsync());
        }
    }
}
