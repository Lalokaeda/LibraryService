﻿// <auto-generated />
using System;
using BookRentService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookRentService.Migrations
{
    [DbContext(typeof(BookRentDbContext))]
    partial class BookRentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookRentService.Domain.Entities.BookExemplarRent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookExemplarId")
                        .HasColumnType("int");

                    b.Property<int>("BookRentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookRentId");

                    b.ToTable("BookExemplarRents");
                });

            modelBuilder.Entity("BookRentService.Domain.Entities.BookRent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RentStatusId")
                        .HasColumnType("int");

                    b.Property<int>("RenterId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RentStatusId");

                    b.HasIndex("RenterId");

                    b.ToTable("BookRents");
                });

            modelBuilder.Entity("BookRentService.Domain.Entities.RentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("RentStatuses");
                });

            modelBuilder.Entity("BookRentService.Domain.Entities.Renter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Renters");
                });

            modelBuilder.Entity("BookRentService.Domain.Entities.BookExemplarRent", b =>
                {
                    b.HasOne("BookRentService.Domain.Entities.BookRent", "BookRent")
                        .WithMany("BookExemplarRents")
                        .HasForeignKey("BookRentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookRent");
                });

            modelBuilder.Entity("BookRentService.Domain.Entities.BookRent", b =>
                {
                    b.HasOne("BookRentService.Domain.Entities.RentStatus", "RentStatus")
                        .WithMany()
                        .HasForeignKey("RentStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookRentService.Domain.Entities.Renter", "Renter")
                        .WithMany("BookRents")
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RentStatus");

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("BookRentService.Domain.Entities.BookRent", b =>
                {
                    b.Navigation("BookExemplarRents");
                });

            modelBuilder.Entity("BookRentService.Domain.Entities.Renter", b =>
                {
                    b.Navigation("BookRents");
                });
#pragma warning restore 612, 618
        }
    }
}
