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

        public DbSet<Renter> Renters {get; set;}
        public DbSet<RentStatus> RentStatuses {get; set;}
        public DbSet<BookRent> BookRents {get; set;}
        public DbSet<BookExemplarRent> BookExemplarRents {get; set;}
        public DbSet<Fine> Fines {get; set;}

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

            modelBuilder.Entity<Fine>()
                .HasOne(f => f.BookRent)
                .WithMany() 
                .HasForeignKey(f => f.BookRentId)
                .OnDelete(DeleteBehavior.Cascade);
           
            OnModelCreatingPartial(modelBuilder);
        }
         partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}