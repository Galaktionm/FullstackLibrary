
using LibraryApp.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions options): base(options) {}

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
                      
            //builder.Entity<Book>().HasKey("_id");
            builder.Entity<Book>().HasMany<Author>(book=>book.Authors).WithMany(author=>author.books);
            builder.Entity<Book>().HasMany<Checkout>(book=>book.Checkouts).WithMany(checkout=>checkout.books);
            builder.Entity<Book>().HasMany<BookReview>(book=>book.Reviews).WithOne().HasForeignKey(review=>review.bookId);
            
            builder.Entity<User>().HasMany<Checkout>(user=>user.checkouts).WithOne(checkout=>checkout.user);
            builder.Entity<User>().HasMany<BookReview>(user=>user.Reviews).WithOne(review=>review.user).HasForeignKey(review=>review.userId);
        
            builder.Entity<BookReview>().HasKey(review=>new { review.bookId, review.userId });
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookReview> Reviews { get; set;}

        public DbSet<Checkout> Checkouts { get; set; }

    }