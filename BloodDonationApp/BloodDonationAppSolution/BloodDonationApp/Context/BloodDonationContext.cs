using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationApp.Context
{
    public class BloodDonationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        //public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<BloodStock> BloodStocks { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<BloodDonation> BloodDonations { get; set; }

        public BloodDonationContext(DbContextOptions<BloodDonationContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasOne(u => u.Admin)
               .WithOne(a => a.User)
               .HasForeignKey<Admin>(a => a.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Donor)
                .WithOne(d => d.User)
                .HasForeignKey<Donor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Recipient)
                .WithOne(r => r.User)
                .HasForeignKey<Recipient>(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Admin to BloodStock relationship
            modelBuilder.Entity<BloodStock>()
                .HasOne(bs => bs.Admin)
                .WithMany(a => a.BloodStocks)
                .HasForeignKey(bs => bs.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Donor to BloodStock, Hospital, Recipient relationship
            modelBuilder.Entity<BloodStock>()
                .HasOne(bs => bs.Donor)
                .WithMany(d => d.BloodStocks)
                .HasForeignKey(bs => bs.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BloodStock>()
                .HasOne(bs => bs.Recipient)
                .WithMany(r => r.BloodStocks)
                .HasForeignKey(bs => bs.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BloodDonation>()
                .HasOne(bd => bd.Recipient)
                .WithMany(r => r.BloodDonations)
                .HasForeignKey(bd => bd.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

           

            modelBuilder.Entity<Hospital>()
                .HasMany(h => h.Donors)
                .WithMany(d => d.Hospitals);

            modelBuilder.Entity<Recipient>()
                .HasMany(r => r.Hospitals)
                .WithMany(h => h.Recipients);

            // Convert enum to string in database
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            // other configurations...
        }
    }
}
