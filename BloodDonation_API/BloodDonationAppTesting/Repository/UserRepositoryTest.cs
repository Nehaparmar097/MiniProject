
using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Models;
using Job_Portal_API.Models.Enums;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_API.Tests.RepositoryTests
{
    public class UserRepositoryTests
    {
        private BloodDonationAppContext _context;
        private UserRepository _userRepository;
        private User _user;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "BloodDonationApp")
                .Options;

            _context = new BloodDonationAppContext(options);
            _userRepository = new UserRepository(_context);

            // Initialize User
            _user = new User
            {
                UserID = 1,
                Email = "john@example.com",
                Password = new byte[] { 1, 2, 3 },
                HashKey = new byte[] { 4, 5, 6 },
                FirstName = "John",
                LastName = "Doe",
                ContactNumber = "1234567890",
                UserType = UserType.Admin
            };
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
            var newUser = new User
            {
                UserID = 2,
                Email = "jane@example.com",
                Password = new byte[] { 1, 2, 3 },
                HashKey = new byte[] { 4, 5, 6 },
                FirstName = "Jane",
                LastName = "Doe",
                ContactNumber = "0987654321",
                UserType = UserType.Admin
            };

            // Act
            var result = await _userRepository.Add(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("jane@example.com", result.Email);
        }

        [Test]
        public void Add_Fail_UserAlreadyExists()
        {
            // Arrange
            var newUser = new User
            {
                UserID = 1,
                Email = "john@example.com", // Existing email
                Password = new byte[] { 1, 2, 3 },
                HashKey = new byte[] { 4, 5, 6 },
                FirstName = "John",
                LastName = "Doe",
                ContactNumber = "1234567890",
                UserType = UserType.Admin
            };

            // Act & Assert
            Assert.ThrowsAsync<UserAlreadyExistException>(async () => await _userRepository.Add(newUser));
        }

        [Test]
        public async Task Update_Success()
        {
            // Arrange
            var updatedUser = new User
            {
                UserID = 1,
                Email = "john@example.com",
                Password = new byte[] { 7, 8, 9 },
                HashKey = new byte[] { 10, 11, 12 },
                FirstName = "John",
                LastName = "Smith",
                ContactNumber = "1234567890",
                UserType = UserType.Admin
            };

            // Act
            var result = await _userRepository.Update(updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Smith", result.LastName);
        }

        [Test]
        public void Update_Fail_UserNotFound()
        {
            // Arrange
            var updatedUser = new User
            {
                UserID = 3, // Non-existing user ID
                Email = "nonexistent@example.com",
                Password = new byte[] { 1, 2, 3 },
                HashKey = new byte[] { 4, 5, 6 },
                FirstName = "Non",
                LastName = "Existent",
                ContactNumber = "1234567890",
                UserType = UserType.Admin
            };

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userRepository.Update(updatedUser));
        }

        [Test]
        public async Task Delete_Success()
        {
            // Act
            var result = await _userRepository.DeleteById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.UserID);
        }

        [Test]
        public void Delete_Fail_UserNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userRepository.DeleteById(3));
        }

        [Test]
        public async Task GetById_Success()
        {
            // Act
            var result = await _userRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("john@example.com", result.Email);
        }

        [Test]
        public void GetById_Fail_UserNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userRepository.GetById(3));
        }

        [Test]
        public async Task GetAll_Success()
        {
            // Act
            var result = await _userRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAll_Fail()
        {
            // Arrange
            _context.Users.RemoveRange(_context.Users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
