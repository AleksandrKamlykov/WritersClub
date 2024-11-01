using Microsoft.EntityFrameworkCore;
using WritersClub.Models;

namespace WritersClub.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
        {




        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("Pages"); // Ensure the table name is correct
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
                entity.HasOne(e => e.Book)
                      .WithMany(b => b.Pages)
                      .HasForeignKey(e => e.BookId);
            });

        }

    }
}
