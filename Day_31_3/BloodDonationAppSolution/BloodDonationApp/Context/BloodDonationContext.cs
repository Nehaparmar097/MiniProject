using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationApp.Context
{
    public class BloodDonationContext:DbContext
    {
        public BloodDonationContext(DbContextOptions<BloodDonationContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<BloodDonation> BloodDonations { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<BloodStock> BloodStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships
            builder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne(u => u.Admin)
                .HasForeignKey<Admin>(a => a.UserId);

            builder.Entity<Donor>()
                .HasOne(d => d.User)
                .WithOne(u => u.Donor)
                .HasForeignKey<Donor>(d => d.UserId);

            builder.Entity<Recipient>()
                .HasOne(r => r.User)
                .WithOne(u => u.Recipient)
                .HasForeignKey<Recipient>(r => r.UserId);

            builder.Entity<BloodDonation>()
                .HasOne(bd => bd.Donor)
                .WithMany(d => d.BloodDonations)
                .HasForeignKey(bd => bd.DonorId);

            builder.Entity<BloodDonation>()
                .HasOne(bd => bd.Recipient)
                .WithMany(r => r.BloodDonations)
                .HasForeignKey(bd => bd.RecipientId);
        }
    }
}
