﻿// <auto-generated />
using System;
using Job_Portal_API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloodDonationApp.Migrations
{
    [DbContext(typeof(BloodDonationAppContext))]
    [Migration("20240529205627_Intial")]
    partial class Intial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Job_Portal_API.Models.BloodDonation", b =>
                {
                    b.Property<int>("BloodDonationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BloodDonationID"), 1L, 1);

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<int>("RecipientID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BloodDonationID");

                    b.HasIndex("ID");

                    b.HasIndex("RecipientID");

                    b.ToTable("BloodDonations");
                });

            modelBuilder.Entity("Job_Portal_API.Models.BloodStock", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Charge")
                        .HasColumnType("float");

                    b.Property<string>("Disease_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disease_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DonorID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("donationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("expiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DonorID");

                    b.ToTable("BloodStocks");
                });

            modelBuilder.Entity("Job_Portal_API.Models.Donor", b =>
                {
                    b.Property<int>("DonorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonorID"), 1L, 1);

                    b.Property<string>("HospitalDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("DonorID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Donors");
                });

            modelBuilder.Entity("Job_Portal_API.Models.DonorBlood", b =>
                {
                    b.Property<int>("DonorBloodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonorBloodID"), 1L, 1);

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<string>("bloodtype")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DonorBloodID");

                    b.HasIndex("ID");

                    b.ToTable("DonorBloods");
                });

            modelBuilder.Entity("Job_Portal_API.Models.Recipient", b =>
                {
                    b.Property<int>("RecipientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipientID"), 1L, 1);

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("RecipientID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("Job_Portal_API.Models.RecipientBlood", b =>
                {
                    b.Property<int>("RecipientBloodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipientBloodID"), 1L, 1);

                    b.Property<int>("RecipientID")
                        .HasColumnType("int");

                    b.Property<string>("bloodtype")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecipientBloodID");

                    b.HasIndex("RecipientID");

                    b.ToTable("RecipientBloods");
                });

            modelBuilder.Entity("Job_Portal_API.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("HashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Job_Portal_API.Models.BloodDonation", b =>
                {
                    b.HasOne("Job_Portal_API.Models.BloodStock", "BloodStock")
                        .WithMany("BloodDonations")
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Job_Portal_API.Models.Recipient", "Recipient")
                        .WithMany("BloodDonations")
                        .HasForeignKey("RecipientID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BloodStock");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("Job_Portal_API.Models.BloodStock", b =>
                {
                    b.HasOne("Job_Portal_API.Models.Donor", "Donor")
                        .WithMany("BloodStocks")
                        .HasForeignKey("DonorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("Job_Portal_API.Models.Donor", b =>
                {
                    b.HasOne("Job_Portal_API.Models.User", "User")
                        .WithOne("Donor")
                        .HasForeignKey("Job_Portal_API.Models.Donor", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Job_Portal_API.Models.DonorBlood", b =>
                {
                    b.HasOne("Job_Portal_API.Models.BloodStock", "BloodStocks")
                        .WithMany("DonorBloods")
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BloodStocks");
                });

            modelBuilder.Entity("Job_Portal_API.Models.Recipient", b =>
                {
                    b.HasOne("Job_Portal_API.Models.User", "User")
                        .WithOne("Recipient")
                        .HasForeignKey("Job_Portal_API.Models.Recipient", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Job_Portal_API.Models.RecipientBlood", b =>
                {
                    b.HasOne("Job_Portal_API.Models.Recipient", "Recipient")
                        .WithMany("RecipientBloods")
                        .HasForeignKey("RecipientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("Job_Portal_API.Models.BloodStock", b =>
                {
                    b.Navigation("BloodDonations");

                    b.Navigation("DonorBloods");
                });

            modelBuilder.Entity("Job_Portal_API.Models.Donor", b =>
                {
                    b.Navigation("BloodStocks");
                });

            modelBuilder.Entity("Job_Portal_API.Models.Recipient", b =>
                {
                    b.Navigation("BloodDonations");

                    b.Navigation("RecipientBloods");
                });

            modelBuilder.Entity("Job_Portal_API.Models.User", b =>
                {
                    b.Navigation("Donor")
                        .IsRequired();

                    b.Navigation("Recipient")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
