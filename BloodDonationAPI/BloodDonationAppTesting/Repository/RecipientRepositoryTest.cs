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
    public class RecipientRepositoryTest
    {
        private readonly RecipientRepository _repository;
        private readonly DbContextOptions<BloodDonationAppContext> _options;

        public RecipientRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new BloodDonationAppContext(_options);
            _repository = new RecipientRepository(context);
        }

        [Fact]
        public async Task Add_ShouldAddRecipient()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                var recipient = new Recipient
                {
                    UserID = 1,
                    Age = 25,
                    RequiredBloodType = "A+",
                    BloodRequiredDate = DateTime.Now
                };

                var result = await _repository.Add(recipient);

                Assert.NotNull(result);
                Assert.Equals(recipient.UserID, result.UserID);
            }
        }

        [Fact]
        public async Task Update_ShouldUpdateRecipient()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                var recipient = new Recipient
                {
                    RecipientID = 1,
                    UserID = 1,
                    Age = 25,
                    RequiredBloodType = "A+",
                    BloodRequiredDate = DateTime.Now
                };
                await context.Recipients.AddAsync(recipient);
                await context.SaveChangesAsync();

                var updatedRecipient = new Recipient
                {
                    RecipientID = 1,
                    UserID = 1,
                    Age = 26,
                    RequiredBloodType = "B+",
                    BloodRequiredDate = DateTime.Now.AddDays(1)
                };

                var result = await _repository.Update(updatedRecipient);

                Assert.NotNull(result);
                Assert.Equals(updatedRecipient.Age, result.Age);
                Assert.Equals(updatedRecipient.RequiredBloodType, result.RequiredBloodType);
            }
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenRecipientNotFound()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                var recipient = new Recipient
                {
                    RecipientID = 1,
                    UserID = 1,
                    Age = 25,
                    RequiredBloodType = "A+",
                    BloodRequiredDate = DateTime.Now
                };

                // await Assert.ThrowsAsync<UserNotFoundException>(() => _repository.Update(recipient));
                 Assert.ThrowsAsync<UserNotFoundException>(async () => await _repository.Update(recipient));

            }
        }

        [Fact]
        public async Task DeleteById_ShouldDeleteRecipient()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                var recipient = new Recipient
                {
                    RecipientID = 1,
                    UserID = 1,
                    Age = 25,
                    RequiredBloodType = "A+",
                    BloodRequiredDate = DateTime.Now
                };
                await context.Recipients.AddAsync(recipient);
                await context.SaveChangesAsync();

                var result = await _repository.DeleteById(recipient.RecipientID);

                Assert.NotNull(result);
                Assert.Equals(recipient.RecipientID, result.RecipientID);
            }
        }

        [Fact]
        public async Task DeleteById_ShouldThrowException_WhenRecipientNotFound()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                var exception =  Assert.ThrowsAsync<UserNotFoundException>(() => _repository.DeleteById(1));

                Assert.NotNull(exception);
            }
        }

        [Fact]
        public async Task GetById_ShouldReturnRecipient()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                var recipient = new Recipient
                {
                    RecipientID = 1,
                    UserID = 1,
                    Age = 25,
                    RequiredBloodType = "A+",
                    BloodRequiredDate = DateTime.Now
                };
                await context.Recipients.AddAsync(recipient);
                await context.SaveChangesAsync();

                var result = await _repository.GetById(recipient.RecipientID);

                Assert.NotNull(result);
                Assert.Equals(recipient.RecipientID, result.RecipientID);
            }
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenRecipientNotFound()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                var exception =  Assert.ThrowsAsync<RecipientNotFoundException>(() => _repository.GetById(1));

                Assert.NotNull(exception);
            }
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllRecipients()
        {
            using (var context = new BloodDonationAppContext(_options))
            {
                var recipients = new List<Recipient>
                {
                    new Recipient
                    {
                        RecipientID = 1,
                        UserID = 1,
                        Age = 25,
                        RequiredBloodType = "A+",
                        BloodRequiredDate = DateTime.Now
                    },
                    new Recipient
                    {
                        RecipientID = 2,
                        UserID = 2,
                        Age = 30,
                        RequiredBloodType = "B+",
                        BloodRequiredDate = DateTime.Now.AddDays(1)
                    }
                };
                await context.Recipients.AddRangeAsync(recipients);
                await context.SaveChangesAsync();

                var result = await _repository.GetAll();

                Assert.NotNull(result);
                Assert.Equals(2, ((List<Recipient>)result).Count);
            }
        }
    }
}
