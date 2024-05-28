namespace BloodDonationApp.Repository
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using BloodDonationApp.Context;
    using BloodDonationApp.Models;
    using BloodDonationApp.Models.DTOs;
    using BloodDonationApp.Repository.Interfaces;

    public class UserRegisterRepository : IUserRegisterRepository
    {
        private readonly BloodDonationContext _context;

        public UserRegisterRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<(Recipient recipient, User user)> AddRecipientUserWithTransaction(UserRegisterRepositoryDTO userRegisterDTO)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newUser = new User
                    {
                        Role = "Recipient",
                        Password = userRegisterDTO.Password,
                        PasswordHashKey = userRegisterDTO.PasswordHashKey
                    };
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    var newRecipient = new Recipient
                    {
                        RecipientId = newUser.UserId,
                        Name = userRegisterDTO.Name,
                        Email = userRegisterDTO.Email,
                        Phone = userRegisterDTO.Phone
                        // Add any additional recipient properties here
                    };
                    await _context.Recipients.AddAsync(newRecipient);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return (newRecipient, newUser);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error while adding recipient user");
                }
            }
        }

        public async Task<(Donor, User)> AddDonorUserWithTransaction(UserRegisterRepositoryDTO userRegisterRepositoryDTO)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = new User
                    {
                      
                        Password = userRegisterRepositoryDTO.Password,
                        PasswordHashKey = userRegisterRepositoryDTO.PasswordHashKey,
                        Role = "Donor"
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    var donor = new Donor
                    {
                        UserId = user.UserId,
                        
                        // add other donor-specific fields here
                    };

                    _context.Donors.Add(donor);
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return (donor, user);
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}


