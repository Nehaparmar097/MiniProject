using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;
using Job_Portal_API.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Tests
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IRepository<int, User>> _mockUserRepository;
        private Mock<IToken> _mockTokenService;
        private Mock<IRepository<int, Recipient>> _mockJobSeekerRepo;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IRepository<int, User>>();
            _mockTokenService = new Mock<IToken>();
            _mockJobSeekerRepo = new Mock<IRepository<int, Recipient>>();

            _userService = new UserService(_mockUserRepository.Object, _mockTokenService.Object, _mockJobSeekerRepo.Object);
        }

        [Test]
        public async Task RegisterUser_Success()
        {
            // Arrange
            var registerDTO = new RegisterUserDTO
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Password = "password",
                UserType = "Recipient",
                ContactNumber = "1234567890"
            };

            _mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>())).ReturnsAsync((User user) => user);

            // Act
            var result = await _userService.RegisterUser(registerDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(registerDTO.Email, result.Email);
            Assert.AreEqual(registerDTO.FirstName + " " + registerDTO.LastName, result.Name);
        }

        [Test]
        public void RegisterUser_InvalidUserType_Fail()
        {
            // Arrange
            var registerDTO = new RegisterUserDTO
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Password = "password",
                UserType = "InvalidType",
                ContactNumber = "1234567890"
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _userService.RegisterUser(registerDTO));
        }

        [Test]
        public async Task LoginUser_Success()
        {
            // Arrange
            var user = new User
            {
                UserID = 1,
                Email = "test@example.com",
                HashKey = new HMACSHA512().Key,
                Password = new HMACSHA512().ComputeHash(Encoding.UTF8.GetBytes("password")),
                UserType = UserType.Recipient
            };

            var loginDTO = new LoginUserDTO
            {
                Email = "test@example.com",
                Password = "password"
            };

            _mockUserRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { user });
            _mockTokenService.Setup(tokenService => tokenService.GenerateJSONWebToken(It.IsAny<User>())).Returns("dummyToken");

            // Act
            var result = await _userService.LoginUser(loginDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserID, result.UserID);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual("dummyToken", result.Token);
        }

        [Test]
        public void LoginUser_InvalidCredentials_Fail()
        {
            // Arrange
            var loginDTO = new LoginUserDTO
            {
                Email = "nonexistent@example.com",
                Password = "password"
            };

            _mockUserRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _userService.LoginUser(loginDTO));
        }

        [Test]
        public async Task GetAllUsers_Success()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserID = 1, Email = "user1@example.com", FirstName = "User", LastName = "One", UserType = UserType.Recipient },
                new User { UserID = 2, Email = "user2@example.com", FirstName = "User", LastName = "Two", UserType = UserType.Donor }
            };

            _mockUserRepository.Setup(repo => repo.GetAll()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("user1@example.com", result.First().Email);
        }

        [Test]
        public void GetAllUsers_NoUsers_Fail()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NoUsersFoundException>(async () => await _userService.GetAllUsers());
        }

        [Test]
        public async Task GetUserById_Success()
        {
            // Arrange
            var user = new User { UserID = 1, Email = "user1@example.com", FirstName = "User", LastName = "One", UserType = UserType.Recipient };

            _mockUserRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Email, result.Email);
        }

        [Test]
        public void GetUserById_UserNotFound_Fail()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userService.GetUserById(1));
        }

        [Test]
        public async Task UpdateUserEmail_Success()
        {
            // Arrange
            var user = new User { UserID = 1, Email = "user1@example.com", FirstName = "User", LastName = "One", UserType = UserType.Recipient };

            _mockUserRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.Update(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.UpdateUserEmail(1, "newemail@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("newemail@example.com", result.Email);
        }

        [Test]
        public void UpdateUserEmail_UserNotFound_Fail()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userService.UpdateUserEmail(1, "newemail@example.com"));
        }

        [Test]
        public async Task DeleteUserById_Success()
        {
            // Arrange
            var user = new User { UserID = 1, Email = "user1@example.com", FirstName = "User", LastName = "One", UserType = UserType.Recipient };

            _mockUserRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync(user);

            // Act
            var result = await _userService.DeleteUserById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Email, result.Email);
        }

        [Test]
        public void DeleteUserById_UserNotFound_Fail()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userService.DeleteUserById(1));
        }
    }
}
