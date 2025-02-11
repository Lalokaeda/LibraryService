using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRentService.Infrastructure
{
    public partial class BookRentDbContext : DbContext
    {
        public BookRentDbContext(DbContextOptions<BookRentDbContext> options)
            : base(options)
        {

        }

        DbSet<Renter> Renters {get; set;}
        DbSet<RentStatus> RentStatuses {get; set;}
        DbSet<BookRent> BookRents {get; set;}
        DbSet<BookExemplarRent> BookExemplarRents {get; set;}

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookRent>()
                .HasOne(e=>e.Renter)
                .WithMany(e=>e.BookRents)
                .HasForeignKey(e=>e.RenterId)
                .IsRequired();

            modelBuilder.Entity<BookExemplarRent>()
                .HasOne(e => e.BookRent)
                .WithMany(e => e.BookExemplarRents)
                .HasForeignKey(r => r.BookRentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
           
            OnModelCreatingPartial(modelBuilder);
        }
         partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}