using Job_Portal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_API.Context
{
    public class BloodDonationAppContext : DbContext
    {
        public BloodDonationAppContext(DbContextOptions options) : base(options)
        {
        }

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=15RBBX3\\SQLEXPRESS;Integrated Security=true;Initial Catalog=BloodDonationApp;");
        }*/
        public DbSet<User> Users { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<BloodStock> BloodStocks { get; set; }
        public DbSet<BloodDonation> BloodDonations { get; set; }
       
        public DbSet<DonorBlood> DonorBloods { get; set; }
        public DbSet<RecipientBlood> RecipientBloods { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<Donor>().HasKey(e => e.DonorID);
            modelBuilder.Entity<Recipient>().HasKey(j => j.RecipientID);
            modelBuilder.Entity<BloodStock>().HasKey(j => j.ID);
            modelBuilder.Entity<BloodDonation>().HasKey(a => a.BloodDonationID);
          
            
            modelBuilder.Entity<DonorBlood>().HasKey(js => js.DonorBloodID );
            modelBuilder.Entity<RecipientBlood>().HasKey(js => js.RecipientBloodID);

           

            modelBuilder.Entity<Donor>()
                .HasOne(e => e.User)
                .WithOne(u => u.Donor)
                .HasForeignKey<Donor>(e => e.UserID);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            
            modelBuilder.Entity<Recipient>()
                .HasOne(j => j.User)
                .WithOne(u => u.Recipient)
                .HasForeignKey<Recipient>(j => j.UserID);

            modelBuilder.Entity<BloodStock>()
                .HasOne(j => j.Donor)
                .WithMany(e => e.BloodStocks)
                .HasForeignKey(j => j.DonorID);

            modelBuilder.Entity<BloodDonation>()
                .HasOne(a => a.Recipient)
                .WithMany(j => j.BloodDonations)
                .HasForeignKey(a => a.RecipientID)
                .OnDelete(DeleteBehavior.Restrict);

          

            modelBuilder.Entity<Recipient>()
                .HasMany(j => j.RecipientBloods)
                .WithOne(js => js.Recipient)
                .HasForeignKey(js => js.RecipientID);


          

           

           

            // Configure UserType enum to be stored as a string
            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasConversion<string>()
                .IsRequired();

          //  Configure JobType enum to be stored as a string
           modelBuilder.Entity<BloodStock>()
                .Property(j => j.status)
                .HasConversion<string>()
                .IsRequired();

    }
    }
}
