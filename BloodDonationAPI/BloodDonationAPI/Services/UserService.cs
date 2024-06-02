﻿/*using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Job_Portal_API.Services
{
    public class UserService : IUser
    {
        private readonly IRepository<int, User> _repository;
        private readonly IToken _tokenService;
        private readonly IRepository<int, Recipient> _jobSeekerRepo;

        public UserService(IRepository<int,User> repository,IToken tokenService, IRepository<int, Recipient> jobSeekerRepo)
        {
            _repository = repository;
            _tokenService = tokenService;
            _jobSeekerRepo = jobSeekerRepo;
        }
        public async Task<ReturnUserDTO> RegisterUser(RegisterUserDTO userDTO)
        {
            try
            {
                User user = MapRegisterUserDTOToUser(userDTO); ;
                if(user.UserType == UserType.Recipient)
                user.Recipient = new Recipient();
                var result= await _repository.Add(user);

                ReturnUserDTO returnUser = new ReturnUserDTO() {UserID=result.UserID, Email = result.Email,Role = result.UserType.ToString(),Name=result.FirstName+result.LastName,ContactNumber=result.ContactNumber,RecipientID=result.Recipient.RecipientID };
                return returnUser;
            }
            catch(UserAlreadyExistException e)
            {
                throw new UserAlreadyExistException();
            }
            catch(ArgumentException e)
            {
                throw new ArgumentException("Please Enter Valid User Type");
            }
        }
        public async Task<ReturnLoginDTO> LoginUser(LoginUserDTO userDTO)
        {
            
            var users = await _repository.GetAll();
            var user = users.FirstOrDefault(u => u.Email == userDTO.Email);
            if (user == null)
            {
                throw new UnauthorizedUserException("Invalid Email or password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(user.HashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, user.Password);
            if (isPasswordSame)
            {
                
                
                ReturnLoginDTO loginReturnDTO = MapUserToLoginReturn(user);
                return loginReturnDTO;
               

                
            }
            throw new UnauthorizedUserException("Invalid username or password");
        }

        public async Task<ReturnUserDTO> DeleteUserById(int id)
        {
            try
            {
                var user = await _repository.DeleteById(id);
                ReturnUserDTO returnUser = new ReturnUserDTO() { UserID = user.UserID, Email = user.Email, Role = user.UserType.ToString(), Name = user.FirstName + user.LastName, ContactNumber = user.ContactNumber };
                return returnUser;
            }
            catch (Exception e)
            {
                throw new UserNotFoundException();
            }
        }

        public async Task<IEnumerable<ReturnUserDTO>> GetAllUsers()
        {
            var users = await _repository.GetAll();
            
            if (users.Count() == 0)
            {
                throw new NoUsersFoundException();
            }
            List<ReturnUserDTO> results = new List<ReturnUserDTO>();
            foreach (var user in users)
            {
                results.Add(new ReturnUserDTO() { UserID = user.UserID, Email = user.Email, Role = user.UserType.ToString(), Name = user.FirstName + user.LastName, ContactNumber = user.ContactNumber });
            }
            return results;
        }

        public async Task<ReturnUserDTO> GetUserById(int id)
        {
            try
            {
                var user = await _repository.GetById(id);
                ReturnUserDTO returnUser = new ReturnUserDTO() { UserID = user.UserID, Email = user.Email, Role = user.UserType.ToString(), Name = user.FirstName + user.LastName, ContactNumber = user.ContactNumber };
                return returnUser;
            }
            catch (Exception e)
            {
                throw new UserNotFoundException();
            }
        }

        public async Task<ReturnUserDTO> UpdateUserEmail(int id, string email)
            {
                try
                {
                    var user = await _repository.GetById(id);
                    user.Email = email;
                    user = await _repository.Update(user);
                ReturnUserDTO returnUser = new ReturnUserDTO() { UserID = user.UserID, Email = user.Email, Role = user.UserType.ToString(), Name = user.FirstName + user.LastName, ContactNumber = user.ContactNumber };
                return returnUser;

            }
                catch (Exception e)
                {
                    throw new UserNotFoundException();
                }
            }
        private User MapRegisterUserDTOToUser(RegisterUserDTO userDTO)
        {
            // Validate and convert UserType
            if (!Enum.TryParse<UserType>(userDTO.UserType, true, out var userType))
            {
                throw new ArgumentException("Invalid user type");
            }
            User user = new User()
            {
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserType = userType,
                ContactNumber = userDTO.ContactNumber,
                
            };
            
            
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.HashKey = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            return user;
        }
        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }
        private ReturnLoginDTO MapUserToLoginReturn(User user)
        {
            ReturnLoginDTO returnDTO = new ReturnLoginDTO();
            returnDTO.UserID = user.UserID;
            returnDTO.Role = user.UserType.ToString();
            returnDTO.Email = user.Email;
            returnDTO.Token = _tokenService.GenerateJSONWebToken(user);
            return returnDTO;
        }

        
    }
}
*/
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Job_Portal_API.Services
{
    public class UserService : IUser
    {
        private readonly IRepository<int, User> _repository;
        private readonly IToken _tokenService;
        private readonly IRepository<int, Recipient> _jobSeekerRepo;

        public UserService(IRepository<int, User> repository, IToken tokenService, IRepository<int, Recipient> jobSeekerRepo)
        {
            _repository = repository;
            _tokenService = tokenService;
            _jobSeekerRepo = jobSeekerRepo;
        }
        private User MapRegisterUserDTOToUser(RegisterUserDTO userDTO)
        {
            // Validate and convert UserType
            if (!Enum.TryParse<UserType>(userDTO.UserType, true, out var userType))
            {
                throw new ArgumentException("Invalid user type");
            }

            // Create a new User object and set its properties
            User user = new User()
            {
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserType = userType,
                ContactNumber = userDTO.ContactNumber,
            };

            // Create the password hash
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                user.HashKey = hmac.Key;
                user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            }

            return user;
        }

        public async Task<ReturnUserDTO> RegisterUser(RegisterUserDTO userDTO)
        {
            try
            {
                User user = MapRegisterUserDTOToUser(userDTO);
                //if (user.UserType == UserType.Recipient)
                //{
                //    user.Recipient = new Recipient();
                //}
                var result = await _repository.Add(user);

                ReturnUserDTO returnUser = new ReturnUserDTO()
                {
                    UserID = result.UserID,
                    Email = result.Email,
                    Role = result.UserType.ToString(),
                    Name = $"{result.FirstName} {result.LastName}",
                    ContactNumber = result.ContactNumber,
                   // RecipientID = result.Recipient?.RecipientID
                };
                return returnUser;
            }
            catch (UserAlreadyExistException)
            {
                throw new UserAlreadyExistException();
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Please Enter Valid User Type");
            }
        }

        public async Task<ReturnLoginDTO> LoginUser(LoginUserDTO userDTO)
        {
            var users = await _repository.GetAll();
            var user = users.FirstOrDefault(u => u.Email == userDTO.Email);
            if (user == null)
            {
                throw new UnauthorizedUserException("Invalid email or password.");
            }

            using (var hmac = new HMACSHA512(user.HashKey))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                if (!ComparePasswords(computedHash, user.Password))
                {
                    throw new UnauthorizedUserException("Invalid email or password.");
                }
            }

            return new ReturnLoginDTO
            {
                UserID = user.UserID,
                Role = user.UserType.ToString(),
                Email = user.Email,
                Token = _tokenService.GenerateJSONWebToken(user)
            };
        }

        public async Task<ReturnUserDTO> DeleteUserById(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            await _repository.DeleteById(id);
            return new ReturnUserDTO
            {
                UserID = user.UserID,
                Email = user.Email,
                Role = user.UserType.ToString(),
                Name = $"{user.FirstName} {user.LastName}",
                ContactNumber = user.ContactNumber
            };
        }

        public async Task<IEnumerable<ReturnUserDTO>> GetAllUsers()
        {
            var users = await _repository.GetAll();
            if (!users.Any())
            {
                throw new NoUsersFoundException("No users found.");
            }

            return users.Select(user => new ReturnUserDTO
            {
                UserID = user.UserID,
                Email = user.Email,
                Role = user.UserType.ToString(),
                Name = $"{user.FirstName} {user.LastName}",
                ContactNumber = user.ContactNumber
            }).ToList();
        }

        public async Task<ReturnUserDTO> GetUserById(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            return new ReturnUserDTO
            {
                UserID = user.UserID,
                Email = user.Email,
                Role = user.UserType.ToString(),
                Name = $"{user.FirstName} {user.LastName}",
                ContactNumber = user.ContactNumber
            };
        }

        public async Task<ReturnUserDTO> UpdateUserEmail(int id, string email)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            user.Email = email;
            var updatedUser = await _repository.Update(user);
            return new ReturnUserDTO
            {
                UserID = updatedUser.UserID,
                Email = updatedUser.Email,
                Role = updatedUser.UserType.ToString(),
                Name = $"{updatedUser.FirstName} {updatedUser.LastName}",
                ContactNumber = updatedUser.ContactNumber
            };
        }

        private bool ComparePasswords(byte[] computedHash, byte[] storedHash)
        {
            if (computedHash.Length != storedHash.Length)
            {
                return false;
            }

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
