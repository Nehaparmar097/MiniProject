using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Models;
using Job_Portal_API.Models.Enums;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPITest.RepositoryTests
{
    public class UserRepositoryTests
    {
        private BloodDonationAppContext _context;
        private UserRepository _userRepository;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
                .UseInMemoryDatabase(databaseName: "testDB")
                .Options;
            _context = new BloodDonationAppContext(options);

            var userLoggerMock = new Mock<ILogger<UserRepository>>();
            _userRepository = new UserRepository(_context);
        }

        [Test]
        public async Task AddUser_Success()
        {
            // Arrange
            var newUser = new User
            {
                Email = "newuser@example.com",
                FirstName = "New",
                LastName = "User",
                ContactNumber = "9876543210",
                Password = Encoding.UTF8.GetBytes("password"),
                HashKey = Encoding.UTF8.GetBytes("hashkey"),
                UserType = UserType.Donor
            };

            // Act
            var result = await _userRepository.Add(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(newUser.Email, result.Email);
        }

        [Test]
        public void AddUser_Failure_UserAlreadyExists()
        {
            // Arrange
            var existingUser = new User
            {
                Email = "existinguser@example.com",
                FirstName = "Existing",
                LastName = "User",
                ContactNumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HashKey = Encoding.UTF8.GetBytes("hashkey"),
                UserType = UserType.Donor
            };

            // Seed the database with the existing user
            _context.Users.Add(existingUser);
            _context.SaveChanges();

            var newUser = new User
            {
                Email = "existinguser@example.com", // Same email as existing user
                FirstName = "New",
                LastName = "User",
                ContactNumber = "9876543210",
                Password = Encoding.UTF8.GetBytes("password"),
                HashKey = Encoding.UTF8.GetBytes("hashkey"),
                UserType = UserType.Donor
            };

            // Act & Assert
            Assert.ThrowsAsync<UserAlreadyExistException>(async () => await _userRepository.Add(newUser));
        }

        [Test]
        public async Task UpdateUser_Success()
        {
            // Arrange
            var existingUser = new User
            {
                Email = "updateuser@example.com",
                FirstName = "Update",
                LastName = "User",
                ContactNumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HashKey = Encoding.UTF8.GetBytes("hashkey"),
                UserType = UserType.Donor
            };

            var addedUser = await _userRepository.Add(existingUser);
            addedUser.FirstName = "Updated";

            // Act
            var result = await _userRepository.Update(addedUser);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Updated", result.FirstName);
        }

        [Test]
        public void UpdateUser_Failure_UserNotFound()
        {
            // Arrange
            var nonExistentUser = new User
            {
                UserID = 999, // Assuming this user does not exist
                Email = "nonexistent@example.com",
                FirstName = "NonExistent",
                LastName = "User",
                ContactNumber = "9876543210",
                Password = Encoding.UTF8.GetBytes("password"),
                HashKey = Encoding.UTF8.GetBytes("hashkey"),
                UserType = UserType.Donor
            };

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userRepository.Update(nonExistentUser));
        }

        [Test]
        public async Task DeleteUserById_Success()
        {
            // Arrange
            var existingUser = new User
            {
                Email = "deleteuser@example.com",
                FirstName = "Delete",
                LastName = "User",
                ContactNumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HashKey = Encoding.UTF8.GetBytes("hashkey"),
                UserType = UserType.Donor
            };

            var addedUser = await _userRepository.Add(existingUser);

            // Act
            var result = await _userRepository.DeleteById(addedUser.UserID);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(addedUser.UserID, result.UserID);
        }

        [Test]
        public void DeleteUserById_Failure_UserNotFound()
        {
            // Arrange
            var nonExistentUserId = 999; // Assuming this user ID does not exist

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userRepository.DeleteById(nonExistentUserId));
        }

        [Test]
        public async Task GetUserById_Success()
        {
            // Arrange
            var existingUser = new User
            {
                Email = "getuser@example.com",
                FirstName = "Get",
                LastName = "User",
                ContactNumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HashKey = Encoding.UTF8.GetBytes("hashkey"),
                UserType = UserType.Donor
            };

            var addedUser = await _userRepository.Add(existingUser);

            // Act
            var result = await _userRepository.GetById(addedUser.UserID);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(addedUser.UserID, result.UserID);
        }

        [Test]
        public void GetUserById_Failure_UserNotFound()
        {
            // Arrange
            var nonExistentUserId = 999; // Assuming this user ID does not exist

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userRepository.GetById(nonExistentUserId));
        }

        [Test]
        public async Task GetAllUsers_Success()
        {
            // Arrange
            var user1 = new User
            {
                Email = "user1@example.com",
                FirstName = "User1",
                LastName = "Test1",
                ContactNumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password1"),
                HashKey = Encoding.UTF8.GetBytes("hashkey1"),
                UserType = UserType.Donor
            };
            var user2 = new User
            {
                Email = "user2@example.com",
                FirstName = "User2",
                LastName = "Test2",
                ContactNumber = "0987654321",
                Password = Encoding.UTF8.GetBytes("password2"),
                HashKey = Encoding.UTF8.GetBytes("hashkey2"),
                UserType = UserType.Donor
            };

            await _userRepository.Add(user1);
            await _userRepository.Add(user2);

            // Act
            var result = await _userRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAllUsers_Failure_NoUsersPresent()
        {
            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userRepository.GetAll());
        }
    }
}