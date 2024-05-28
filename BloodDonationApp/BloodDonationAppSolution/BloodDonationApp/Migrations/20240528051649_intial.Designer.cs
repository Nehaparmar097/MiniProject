﻿// <auto-generated />
using System;
using BloodDonationApp.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloodDonationApp.Migrations
{
    [DbContext(typeof(BloodDonationContext))]
    [Migration("20240528051649_intial")]
    partial class intial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BloodDonationApp.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AdminId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BloodDonationApp.Models.BloodDonation", b =>
                {
                    b.Property<int>("BloodDonationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BloodDonationId"), 1L, 1);

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DonationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.HasKey("BloodDonationId");

                    b.HasIndex("DonorId");

                    b.HasIndex("RecipientId");

                    b.ToTable("BloodDonations");
                });

            modelBuilder.Entity("BloodDonationApp.Models.BloodStock", b =>
                {
                    b.Property<int>("BloodStockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BloodStockId"), 1L, 1);

                    b.Property<int?>("AdminId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DonorId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("RecipientId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasKey("BloodStockId");

                    b.HasIndex("AdminId");

                    b.HasIndex("DonorId");

                    b.HasIndex("RecipientId");

                    b.ToTable("BloodStocks");
                });

            modelBuilder.Entity("BloodDonationApp.Models.Donor", b =>
                {
                    b.Property<int>("DonorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonorId"), 1L, 1);

                    b.Property<string>("AvailabilityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DonationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DonorId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Donors");
                });

            modelBuilder.Entity("BloodDonationApp.Models.Hospital", b =>
                {
                    b.Property<int>("HospitalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HospitalId"), 1L, 1);

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pincode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HospitalId");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("BloodDonationApp.Models.Recipient", b =>
                {
                    b.Property<int>("RecipientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipientId"), 1L, 1);

                    b.Property<string>("BloodRequirementDetail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DonorId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RecipientId");

                    b.HasIndex("DonorId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("BloodDonationApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DonorHospital", b =>
                {
                    b.Property<int>("DonorsDonorId")
                        .HasColumnType("int");

                    b.Property<int>("HospitalsHospitalId")
                        .HasColumnType("int");

                    b.HasKey("DonorsDonorId", "HospitalsHospitalId");

                    b.HasIndex("HospitalsHospitalId");

                    b.ToTable("DonorHospital");
                });

            modelBuilder.Entity("HospitalRecipient", b =>
                {
                    b.Property<int>("HospitalsHospitalId")
                        .HasColumnType("int");

                    b.Property<int>("RecipientsRecipientId")
                        .HasColumnType("int");

                    b.HasKey("HospitalsHospitalId", "RecipientsRecipientId");

                    b.HasIndex("RecipientsRecipientId");

                    b.ToTable("HospitalRecipient");
                });

            modelBuilder.Entity("BloodDonationApp.Models.Admin", b =>
                {
                    b.HasOne("BloodDonationApp.Models.User", "User")
                        .WithOne("Admin")
                        .HasForeignKey("BloodDonationApp.Models.Admin", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BloodDonationApp.Models.BloodDonation", b =>
                {
                    b.HasOne("BloodDonationApp.Models.Donor", "Donor")
                        .WithMany()
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BloodDonationApp.Models.Recipient", "Recipient")
                        .WithMany("BloodDonations")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Donor");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("BloodDonationApp.Models.BloodStock", b =>
                {
                    b.HasOne("BloodDonationApp.Models.Admin", "Admin")
                        .WithMany("BloodStocks")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BloodDonationApp.Models.Donor", "Donor")
                        .WithMany("BloodStocks")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BloodDonationApp.Models.Recipient", "Recipient")
                        .WithMany("BloodStocks")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Donor");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("BloodDonationApp.Models.Donor", b =>
                {
                    b.HasOne("BloodDonationApp.Models.User", "User")
                        .WithOne("Donor")
                        .HasForeignKey("BloodDonationApp.Models.Donor", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BloodDonationApp.Models.Recipient", b =>
                {
                    b.HasOne("BloodDonationApp.Models.Donor", null)
                        .WithMany("Recipients")
                        .HasForeignKey("DonorId");

                    b.HasOne("BloodDonationApp.Models.User", "User")
                        .WithOne("Recipient")
                        .HasForeignKey("BloodDonationApp.Models.Recipient", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DonorHospital", b =>
                {
                    b.HasOne("BloodDonationApp.Models.Donor", null)
                        .WithMany()
                        .HasForeignKey("DonorsDonorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BloodDonationApp.Models.Hospital", null)
                        .WithMany()
                        .HasForeignKey("HospitalsHospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HospitalRecipient", b =>
                {
                    b.HasOne("BloodDonationApp.Models.Hospital", null)
                        .WithMany()
                        .HasForeignKey("HospitalsHospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BloodDonationApp.Models.Recipient", null)
                        .WithMany()
                        .HasForeignKey("RecipientsRecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BloodDonationApp.Models.Admin", b =>
                {
                    b.Navigation("BloodStocks");
                });

            modelBuilder.Entity("BloodDonationApp.Models.Donor", b =>
                {
                    b.Navigation("BloodStocks");

                    b.Navigation("Recipients");
                });

            modelBuilder.Entity("BloodDonationApp.Models.Recipient", b =>
                {
                    b.Navigation("BloodDonations");

                    b.Navigation("BloodStocks");
                });

            modelBuilder.Entity("BloodDonationApp.Models.User", b =>
                {
                    b.Navigation("Admin")
                        .IsRequired();

                    b.Navigation("Donor")
                        .IsRequired();

                    b.Navigation("Recipient")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
