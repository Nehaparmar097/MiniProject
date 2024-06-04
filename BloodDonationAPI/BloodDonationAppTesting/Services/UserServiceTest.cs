using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;
using Job_Portal_API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Job_Portal_API.Tests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IRepository<int, User>> _mockUserRepository;
        private readonly Mock<IToken> _mockTokenService;
        private readonly Mock<IRepository<int, Recipient>> _mockRecipientRepository;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<IRepository<int, User>>();
            _mockTokenService = new Mock<IToken>();
            _mockRecipientRepository = new Mock<IRepository<int, Recipient>>();
            _userService = new UserService(_mockUserRepository.Object, _mockTokenService.Object, _mockRecipientRepository.Object);
        }

        [Fact]
        public async Task RegisterUser_ShouldRegisterUser()
        {
            // Arrange
            var userDTO = new RegisterUserDTO
            {
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                UserType = "Recipient",
                ContactNumber = "1234567890",
                Password = "password"
            };
            var user = new User
            {
                UserID = 1,
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                UserType = UserType.Recipient,
                ContactNumber = "1234567890"
            };
            _mockUserRepository.Setup(r => r.Add(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.RegisterUser(userDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(user.Email, result.Email);
        }

        [Fact]
        public async Task RegisterUser_ShouldThrowUserAlreadyExistException()
        {
            // Arrange
            var userDTO = new RegisterUserDTO
            {
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                UserType = "Recipient",
                ContactNumber = "1234567890",
                Password = "password"
            };
            _mockUserRepository.Setup(r => r.Add(It.IsAny<User>())).ThrowsAsync(new UserAlreadyExistException());

            // Act & Assert
             Assert.ThrowsAsync<UserAlreadyExistException>(() => _userService.RegisterUser(userDTO));
        }

        [Fact]
        public async Task LoginUser_ShouldLoginUser()
        {
            // Arrange
            var userDTO = new LoginUserDTO
            {
                Email = "test@test.com",
                Password = "password"
            };
            var user = new User
            {
                UserID = 1,
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                UserType = UserType.Recipient,
                ContactNumber = "1234567890",
                HashKey = new HMACSHA512().Key,
                Password = new HMACSHA512().ComputeHash(Encoding.UTF8.GetBytes("password"))
            };
            _mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<User> { user });
            _mockTokenService.Setup(t => t.GenerateJSONWebToken(It.IsAny<User>())).Returns("token");

            // Act
            var result = await _userService.LoginUser(userDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(user.Email, result.Email);
        }

        [Fact]
        public async Task LoginUser_ShouldThrowUnauthorizedUserException_WhenUserNotFound()
        {
            // Arrange
            var userDTO = new LoginUserDTO
            {
                Email = "test@test.com",
                Password = "password"
            };
            _mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<User>());

            // Act & Assert
             Assert.ThrowsAsync<UserNotFoundException>(() => _userService.LoginUser(userDTO));
        }

        [Fact]
        public async Task DeleteUserById_ShouldDeleteUser()
        {
            // Arrange
            var user = new User
            {
                UserID = 1,
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                UserType = UserType.Recipient,
                ContactNumber = "1234567890"
            };
            _mockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(user);
            _mockUserRepository.Setup(r => r.DeleteById(It.IsAny<int>())).ReturnsAsync(user);

            // Act
            var result = await _userService.DeleteUserById(user.UserID);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(user.Email, result.Email);
        }

        [Fact]
        public async Task DeleteUserById_ShouldThrowUserNotFoundException()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(() => _userService.DeleteUserById(1));
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    UserID = 1,
                    Email = "test1@test.com",
                    FirstName = "John",
                    LastName = "Doe",
                    UserType = UserType.Recipient,
                    ContactNumber = "1234567890"
                },
                new User
                {
                    UserID = 2,
                    Email = "test2@test.com",
                    FirstName = "Jane",
                    LastName = "Doe",
                    UserType = UserType.Recipient,
                    ContactNumber = "0987654321"
                }
            };
            _mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equals(2, result.Count());
        }

        [Fact]
        public async Task GetAllUsers_ShouldThrowNoUsersFoundException()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<User>());

            // Act & Assert
             Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetAllUsers());
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser()
        {
            // Arrange
            var user = new User
            {
                UserID = 1,
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                UserType = UserType.Recipient,
                ContactNumber = "1234567890"
            };
            _mockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserById(user.UserID);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserById_ShouldThrowUserNotFoundException()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

            // Act & Assert
             Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetUserById(1));
        }

        [Fact]
        public async Task UpdateUserEmail_ShouldUpdateEmail()
        {
            // Arrange
            var user = new User
            {
                UserID = 1,
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                UserType = UserType.Recipient,
                ContactNumber = "1234567890"
            };
            var newEmail = "updated@test.com";
            _mockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(user);
            _mockUserRepository.Setup(r => r.Update(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.UpdateUserEmail(user.UserID, newEmail);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(newEmail, result.Email);
        }

        [Fact]
        public async Task UpdateUserEmail_ShouldThrowUserNotFoundException()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

            // Act & Assert
             Assert.ThrowsAsync<UserNotFoundException>(() => _userService.UpdateUserEmail(1, "updated@test.com"));
        }
    }
}
