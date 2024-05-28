using BloodDonationApp.Models.DTOs;
using BloodDonationApp.Models;
using System.Security.Cryptography;
using System.Text;
using BloodDonationApp.Repository.Interfaces;
using BloodDonationApp.Repository;
using BloodDonationApp.Service.interfaces;
using BloodDonationApp.Service.interfaces;
namespace BloodDonationApp.Service
{ 
   /* public class UserService : IUserService
    {
        private readonly IUserRegisterRepository _userRegisterRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecipientRepository _recipientRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository,
            IRecipientRepository recipientRepository,
            IDonorRepository donorRepository,
            IUserRegisterRepository userRegisterRepository,
            ITokenService tokenService
            )
        {
            _recipientRepository = recipientRepository;
            _userRepository = userRepository;
            _userRegisterRepository = userRegisterRepository;
            _tokenService = tokenService;
            _donorRepository = donorRepository;
        }

        #region EncryptPassword
        /// <summary>
        /// Generate encrypted password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private byte[] EncryptPassword(string password, byte[] passwordSalt)
        {
            HMACSHA512 hMACSHA = new HMACSHA512(passwordSalt);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(password));
            return encrypterPass;
        }
        #endregion EncryptPassword

        #region ComparePassword
        /// <summary>
        /// Compare password
        /// </summary>
        /// <param name="encrypterPass"></param>
        /// <param name="password"></param>
        /// <returns>If Match True else False</returns>
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
        #endregion ComparePassword

        #region MapUserToLoginReturn
        /// <summary>
        /// Map User to LoginReturnDTO
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return LoginReturnDTO</returns>
        private LoginReturnDTO MapUserToLoginReturn(User user)
        {
            LoginReturnDTO returnDTO = new LoginReturnDTO();
            returnDTO.UserId = user.UserId;
            returnDTO.Role = user.Role;
            returnDTO.Token = _tokenService.GenerateToken(user);
            return returnDTO;
        }
        #endregion MapUserToLoginReturn

        #region RecipientLogin
        /// <summary>
        /// Recipient Login
        /// </summary>
        /// <param name="userLoginDTO"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NotAbelToLoginException"></exception>
        public async Task<LoginReturnDTO> RecipientLogin(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userRepository.GetRecipientUserByEmail(userLoginDTO.Email);
                if (user == null)
                {
                    throw new UnauthorizedUserException("Invalid Email or Password");
                }
                var encryptedPassword = EncryptPassword(userLoginDTO.Password, user.PasswordHashKey);
                bool isPasswordSame = ComparePassword(encryptedPassword, user.Password);
                if (isPasswordSame)
                {
                    if (user.Recipient == null)
                    {
                        throw new UnauthorizedUserException("Invalid Email or Password");
                    }
                    LoginReturnDTO loginReturnDTO = MapUserToLoginReturn(user);
                    if (loginReturnDTO == null)
                    {
                        throw new NotAbelToLoginException("Error while generating token");
                    }
                    return loginReturnDTO;
                }
                throw new UnauthorizedUserException("Invalid Email or Password");
            }
            catch (RecipientNotFoundException)
            {
                throw new UnauthorizedUserException("Invalid Email or Password");
            }
            catch (Exception e)
            {
                throw new NotAbelToLoginException(e.Message);
            }
        }
        #endregion RecipientLogin

        #region DonorLogin
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLoginDTO"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NotAbelToLoginException"></exception>
        public async Task<LoginReturnDTO> DonorLogin(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userRepository.GetDonorUserByEmail(userLoginDTO.Email);
                if (user == null)
                {
                    throw new UnauthorizedUserException("Invalid Email or Password");
                }
                var encryptedPassword = EncryptPassword(userLoginDTO.Password, user.PasswordHashKey);
                bool isPasswordSame = ComparePassword(encryptedPassword, user.Password);
                if (isPasswordSame)
                {
                    if (user.Donor == null)
                    {
                        throw new UnauthorizedUserException("Invalid Email or Password");
                    }
                    LoginReturnDTO loginReturnDTO = MapUserToLoginReturn(user);
                    if (loginReturnDTO == null)
                    {
                        throw new NotAbelToLoginException("Error while generating token");
                    }
                    return loginReturnDTO;
                }
                throw new UnauthorizedUserException("Invalid Email or Password");
            }
            catch (DonorNotFoundException)
            {
                throw new UnauthorizedUserException("Invalid Email or Password");
            }
            catch (Exception e)
            {
                throw new NotAbelToLoginException(e.Message);
            }

        }
        #endregion DonorLogin

        #region MapRecipientToRegisterReturn
        /// <summary>
        /// Map Recipient to RegisterReturnDTO
        /// </summary>
        /// <param name="user"></param>
        /// <param name="recipient"></param>
        /// <returns></returns>
        private RegisterReturnDTO MapRecipientToRegisterReturn(User user, Recipient recipient)
        {
            RegisterReturnDTO returnDTO = new RegisterReturnDTO();
            returnDTO.Name = recipient.Name;
            returnDTO.Email = recipient.Email;
            returnDTO.Phone = recipient.Phone;
            returnDTO.Role = user.Role;
            returnDTO.Token = _tokenService.GenerateToken(user);
            return returnDTO;
        }
        #endregion MapRecipientToRegisterReturn

        #region MapDonorToRegisterReturn
        /// <summary>
        /// Map Donor to RegisterReturnDTO
        /// </summary>
        /// <param name="user"></param>
        /// <param name="donor"></param>
        /// <returns></returns>
        private RegisterReturnDTO MapDonorToRegisterReturn(User user, Donor donor)
        {
            RegisterReturnDTO returnDTO = new RegisterReturnDTO();
            returnDTO.Name = donor.Name;
            returnDTO.Email = donor.Email;
            returnDTO.Phone = donor.Phone;
            returnDTO.Role = user.Role;
            returnDTO.Token = _tokenService.GenerateToken(user);
            return returnDTO;
        }
        #endregion MapDonorToRegisterReturn

        #region MapUserRegisterRepositoryDTO
        /// <summary>
        /// Map UserRegisterDTO to UserRegisterRepositoryDTO
        /// </summary>
        /// <param name="userRegisterDTO"></param>
        /// <returns></returns>
        private UserRegisterRepositoryDTO MapUserRegisterRepositoryDTO(UserRegisterDTO userRegisterDTO)
        {
            UserRegisterRepositoryDTO userRegisterRepositoryDTO = new UserRegisterRepositoryDTO();
            HMACSHA512 hMACSHA = new HMACSHA512();
            userRegisterRepositoryDTO.PasswordHashKey = hMACSHA.Key;
            userRegisterRepositoryDTO.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDTO.Password));
            userRegisterRepositoryDTO.Name = userRegisterDTO.Name;
            userRegisterRepositoryDTO.Email = userRegisterDTO.Email;
            userRegisterRepositoryDTO.Phone = userRegisterDTO.Phone;
            return userRegisterRepositoryDTO;
        }
        #endregion MapUserRegisterRepositoryDTO

        #region RecipientRegister
        /// <summary>
        /// Recipient Register
        /// </summary>
        /// <param name="userRegisterDTO"></param>
        /// <returns></returns>
        /// <exception cref="UserAlreadyExistsException"></exception>
        /// <exception cref="PasswordMismatchException"></exception>
        /// <exception cref="UnableToRegisterException"></exception>
        public async Task<RegisterReturnDTO> RecipientRegister(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var existingRecipient = await _recipientRepository.GetRecipientByEmail(userRegisterDTO.Email);
                if (existingRecipient != null)
                {
                    throw new UserAlreadyExistsException("Email already exists");
                }
                if (userRegisterDTO.Password != userRegisterDTO.ConfirmPassword)
                {
                    throw new PasswordMismatchException("Password and Confirm Password do not match");
                }
                UserRegisterRepositoryDTO userRegisterRepositoryDTO = MapUserRegisterRepositoryDTO(userRegisterDTO);
                var (recipient, user) = await _userRegisterRepository.AddRecipientUserWithTransaction(userRegisterRepositoryDTO);

                if (recipient == null || user == null)
                {
                    throw new UnableToRegisterException("Error while adding user");
                }
                RegisterReturnDTO registerReturnDTO = MapRecipientToRegisterReturn(user, recipient);
                return registerReturnDTO;
            }
            catch (Exception e)
            {
                throw new UnableToRegisterException(e.Message);
            }
        }
        #endregion RecipientRegister

        #region DonorRegister
        /// <summary>
        /// Donor Register
        /// </summary>
        /// <param name="userRegisterDTO"></param>
        /// <returns></returns>
        /// <exception cref="UserAlreadyExistsException"></exception>
        /// <exception cref="PasswordMismatchException"></exception>
        /// <exception cref="UnableToRegisterException"></exception>
        public async Task<RegisterReturnDTO> DonorRegister(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var existingDonor = await _donorRepository.GetDonorByEmail(userRegisterDTO.Email);
                if (existingDonor != null)
                {
                    throw new UserAlreadyExistsException("Email already exists");
                }
                if (userRegisterDTO.Password != userRegisterDTO.ConfirmPassword)
                {
                    throw new PasswordMismatchException("Password and Confirm Password do not match");
                }
                UserRegisterRepositoryDTO userRegisterRepositoryDTO = MapUserRegisterRepositoryDTO(userRegisterDTO);
                var (donor, user) = await _userRegisterRepository.AddDonorUserWithTransaction(userRegisterRepositoryDTO);

                if (donor == null || user == null)
                {
                    throw new UnableToRegisterException("Error while adding user");
                }
                RegisterReturnDTO registerReturnDTO = MapDonorToRegisterReturn(user, donor);
                return registerReturnDTO;
            }
            catch (Exception e)
            {
                throw new UnableToRegisterException(e.Message);
            }
        }

        Task<UserRegisterReturnDTO> IUserService.RecipientRegister(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }

        Task<UserRegisterReturnDTO> IUserService.DonorRegister(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }
        #endregion DonorRegister

    }*/
   public class UserService : IUserService
    {
        private readonly IUserRegisterRepository _userRegisterRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecipientRepository _recipientRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository,
            IRecipientRepository recipientRepository,
            IDonorRepository donorRepository,
            IUserRegisterRepository userRegisterRepository,
            ITokenService tokenService)
        {
            _recipientRepository = recipientRepository;
            _userRepository = userRepository;
            _userRegisterRepository = userRegisterRepository;
            _tokenService = tokenService;
            _donorRepository = donorRepository;
        }

        #region EncryptPassword
        private byte[] EncryptPassword(string password, byte[] passwordSalt)
        {
            using (HMACSHA512 hMACSHA = new HMACSHA512(passwordSalt))
            {
                var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(password));
                return encrypterPass;
            }
        }
        #endregion EncryptPassword

        #region ComparePassword
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
        #endregion ComparePassword

        #region MapUserToLoginReturn
        private LoginReturnDTO MapUserToLoginReturn(User user)
        {
            return new LoginReturnDTO
            {
                UserId = user.UserId,
                Role = user.Role,
                Token = _tokenService.GenerateToken(user)
            };
        }
        #endregion MapUserToLoginReturn

        #region RecipientLogin
        public async Task<LoginReturnDTO> RecipientLogin(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userRepository.GetRecipientUserByEmail(userLoginDTO.Email);
                if (user == null || user.Recipient == null || !ComparePassword(EncryptPassword(userLoginDTO.Password, user.PasswordHashKey), user.Password))
                {
                    throw new UnauthorizedUserException("Invalid Email or Password");
                }
                return MapUserToLoginReturn(user);
            }
            catch (Exception e)
            {
                throw new NotAbelToLoginException(e.Message);
            }
        }
        #endregion RecipientLogin

        #region DonorLogin
        public async Task<LoginReturnDTO> DonorLogin(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userRepository.GetDonorUserByEmail(userLoginDTO.Email);
                if (user == null || user.Donor == null || !ComparePassword(EncryptPassword(userLoginDTO.Password, user.PasswordHashKey), user.Password))
                {
                    throw new UnauthorizedUserException("Invalid Email or Password");
                }
                return MapUserToLoginReturn(user);
            }
            catch (Exception e)
            {
                throw new NotAbelToLoginException(e.Message);
            }
        }
        #endregion DonorLogin

        #region MapRecipientToRegisterReturn
        private RegisterReturnDTO MapRecipientToRegisterReturn(User user, Recipient recipient)
        {
            return new RegisterReturnDTO
            {
                Name = recipient.Name,
                Email = recipient.Email,
                Phone = recipient.Phone,
                Role = user.Role,
                Token = _tokenService.GenerateToken(user)
            };
        }
        #endregion MapRecipientToRegisterReturn

        #region MapDonorToRegisterReturn
        private RegisterReturnDTO MapDonorToRegisterReturn(User user, Donor donor)
        {
            return new RegisterReturnDTO
            {
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone,
                Role = user.Role,
                Token = _tokenService.GenerateToken(user)
            };
        }
        #endregion MapDonorToRegisterReturn

        #region MapUserRegisterRepositoryDTO
        private UserRegisterRepositoryDTO MapUserRegisterRepositoryDTO(UserRegisterDTO userRegisterDTO)
        {
            using (HMACSHA512 hMACSHA = new HMACSHA512())
            {
                return new UserRegisterRepositoryDTO
                {
                    PasswordHashKey = hMACSHA.Key,
                    Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDTO.Password)),
                    Name = userRegisterDTO.Name,
                    Email = userRegisterDTO.Email,
                    Phone = userRegisterDTO.Phone
                };
            }
        }
        #endregion MapUserRegisterRepositoryDTO

        #region RecipientRegister
        public async Task<RegisterReturnDTO> RecipientRegister(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var existingRecipient = await _recipientRepository.GetRecipientByEmail(userRegisterDTO.Email);
                if (existingRecipient != null)
                {
                    throw new UserAlreadyExistsException("Email already exists");
                }
                if (userRegisterDTO.Password != userRegisterDTO.ConfirmPassword)
                {
                    throw new PasswordMismatchException("Password and Confirm Password do not match");
                }
                UserRegisterRepositoryDTO userRegisterRepositoryDTO = MapUserRegisterRepositoryDTO(userRegisterDTO);
                var (recipient, user) = await _userRegisterRepository.AddRecipientUserWithTransaction(userRegisterRepositoryDTO);

                if (recipient == null || user == null)
                {
                    throw new UnableToRegisterException("Error while adding user");
                }
                return MapRecipientToRegisterReturn(user, recipient);
            }
            catch (Exception e)
            {
                throw new UnableToRegisterException(e.Message);
            }
        }
        #endregion RecipientRegister

        #region DonorRegister
        public async Task<RegisterReturnDTO> DonorRegister(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                // Check if donor already exists
                var existingDonor = await _userRepository.GetDonorUserByEmail(userRegisterDTO.Email);
                if (existingDonor != null)
                {
                    throw new UserAlreadyExistsException("A donor with this email already exists.");
                }

                // Validate password
                if (userRegisterDTO.Password != userRegisterDTO.ConfirmPassword)
                {
                    throw new PasswordMismatchException("Password and Confirm Password do not match.");
                }

                // Encrypt password
                var passwordSalt = GenerateSalt();
                var encryptedPassword = EncryptPassword(userRegisterDTO.Password, passwordSalt);

                // Map DTO to repository DTO
                var userRegisterRepositoryDTO = new UserRegisterRepositoryDTO
                {
                    PasswordHashKey = passwordSalt,
                    Password = encryptedPassword,
                    Name = userRegisterDTO.Name,
                    Email = userRegisterDTO.Email,
                    Phone = userRegisterDTO.Phone
                };

                // Register donor
                var (donor, user) = await _userRegisterRepository.AddDonorUserWithTransaction(userRegisterRepositoryDTO);

                if (donor == null || user == null)
                {
                    throw new UnableToRegisterException("Failed to register donor.");
                }

                // Map donor to return DTO
                var registerReturnDTO = new RegisterReturnDTO
                {
                    Name = donor.Name,
                    Email = donor.Email,
                    Phone = donor.Phone,
                    Role = user.Role,
                    Token = _tokenService.GenerateToken(user)
                };

                return registerReturnDTO;
            }
            catch (Exception ex)
            {
                throw new UnableToRegisterException(ex.Message);
            }
        }

        // Other methods...

       

        private byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
    }

    #endregion DonorRegister
}

