using LibraryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure
{
    public partial class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookExemplar> BookExemplars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
        .HasMany(b => b.Authors)
        .WithMany(/*a => a.Books*/);
        //.UsingEntity<Dictionary<string, object>>(
         //   "Books_Authors",
         //   x => x.HasOne<Book>().WithMany().HasForeignKey("BookId").HasConstraintName("FK_Books_Authors_Book").OnDelete(DeleteBehavior.Cascade),
         //   x => x.HasOne<Author>().WithMany().HasForeignKey("AuthorId").HasConstraintName("FK_Books_Authors_Author").OnDelete(DeleteBehavior.Cascade)
        //);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
