using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Models;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Job_Portal_API.Tests.Repositories
{
    public class BloodDonationRepositoryTests
    {
        private BloodDonationRepository _repository;
        private readonly DbContextOptions<BloodDonationAppContext> _options;

        public BloodDonationRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new BloodDonationAppContext(_options);
            _repository = new BloodDonationRepository(context);
        }

        [Fact]
        public async Task Add_ShouldAddBloodDonation()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                // Arrange
                var bloodDonation = new BloodDonation
                {
                    BloodDonationID = 1,
                    BloodStockID = 1,
                    RecipientID = 1,
                    DonationDate = DateTime.Now,
                    BloodType = "A+"
                };

                // Act
                var result = await _repository.Add(bloodDonation);

                // Assert
                Assert.NotNull(result);
                Assert.Equals(bloodDonation.BloodDonationID, result.BloodDonationID);
            }
        }

    /*    [Fact]
        public async Task Update_ShouldThrowBloodDonationNotFoundException_WhenBloodDonationNotFound()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                // Arrange
                var bloodDonation = new BloodDonation
                {
                    BloodDonationID = 999, // Assuming this ID doesn't exist in the database
                    BloodStockID = 1,
                    RecipientID = 1,
                    DonationDate = DateTime.Now,
                    BloodType = "A+"
                };

                // Act & Assert
                await Assert.ThrowsAsync<BloodDonationNotFoundException>(async () => await _repository.Update(bloodDonation));
            }
        }

        [Fact]
        public async Task DeleteById_ShouldThrowBloodDonationNotFoundException_WhenBloodDonationNotFound()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                // Act & Assert
                await Assert.ThrowsAsync<BloodDonationNotFoundException>(async () => await _repository.DeleteById(999));
            }
        }

        [Fact]
        public async Task GetById_ShouldThrowBloodDonationNotFoundException_WhenBloodDonationNotFound()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                // Act & Assert
                await Assert.ThrowsAsync<BloodDonationNotFoundException>(async () => await _repository.GetById(999));
            }
        }
    */
        [Fact]
        public async Task GetAll_ShouldReturnListOfBloodDonations()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                // Arrange
                var bloodDonations = new List<BloodDonation>
                {
                    new BloodDonation
                    {
                        BloodDonationID = 1,
                        BloodStockID = 1,
                        RecipientID = 1,
                        DonationDate = DateTime.Now,
                        BloodType = "A+"
                    },
                    new BloodDonation
                    {
                        BloodDonationID = 2,
                        BloodStockID = 2,
                        RecipientID = 2,
                        DonationDate = DateTime.Now,
                        BloodType = "B+"
                    }
                };
                await context.BloodDonations.AddRangeAsync(bloodDonations);
                await context.SaveChangesAsync();

                // Act
                var result = await _repository.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.Equals(2, ((List<BloodDonation>)result).Count);
            }
        }
    }
}
