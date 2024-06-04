using Job_Portal_API.Context;
using Job_Portal_API.Models;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Job_Portal_API.Tests.Repositories
{
    public class BloodStockRepositoryTest
    {
        private BloodDonationAppContext _context;
        private BloodStockRepository _repository;

        public BloodStockRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new BloodDonationAppContext(options);
            _repository = new BloodStockRepository(_context);
        }

        [Fact]
        public async Task Add_ShouldAddBloodStock()
        {
            // Arrange
            var bloodStock = new BloodStock
            {
                BloodType = "A+",
                status = "Available",
                city = "City",
                state = "State",
                hospitalName = "Hospital",
                donationDate = DateTime.Now,
                DonorID = 1
            };

            // Act
            var result = await _repository.Add(bloodStock);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(bloodStock.BloodType, result.BloodType);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenBloodStockNotFound()
        {
            // Arrange
            var bloodStock = new BloodStock
            {
                ID = 1,
                BloodType = "A+",
                status = "Available",
                city = "City",
                state = "State",
                hospitalName = "Hospital",
                donationDate = DateTime.Now,
                DonorID = 1
            };

            // Act & Assert
            var exception =  Assert.ThrowsAsync<Exception>(() => _repository.Update(bloodStock));
            Assert.Contains("Custom error message", (System.Collections.ICollection?)exception);

        }

       

        [Fact]
        public async Task GetById_ShouldReturnBloodStock()
        {
            // Arrange
            var bloodStock = new BloodStock
            {
                BloodType = "A+",
                status = "Available",
                city = "City",
                state = "State",
                hospitalName = "Hospital",
                donationDate = DateTime.Now,
                DonorID = 1
            };
            await _context.BloodStocks.AddAsync(bloodStock);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetById(bloodStock.ID);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(bloodStock.ID, result.ID);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllBloodStocks()
        {
            // Arrange
            var bloodStocks = new List<BloodStock>
            {
                new BloodStock
                {
                    BloodType = "A+",
                    status = "Available",
                    city = "City",
                    state = "State",
                    hospitalName = "Hospital",
                    donationDate = DateTime.Now,
                    DonorID = 1
                },
                new BloodStock
                {
                    BloodType = "B+",
                    status = "Available",
                    city = "City",
                    state = "State",
                    hospitalName = "Hospital",
                    donationDate = DateTime.Now,
                    DonorID = 2
                }
            };
            await _context.BloodStocks.AddRangeAsync(bloodStocks);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equals(2, ((List<BloodStock>)result).Count);
        }
    }
}
