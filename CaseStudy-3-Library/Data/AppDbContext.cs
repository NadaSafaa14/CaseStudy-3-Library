using System.Numerics;
using AutoMapper.Execution;
using CaseStudy_3_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy_3_Library.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Book> Books { get; set; }
        public DbSet<Membeer> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Borrowing> Borrowings  { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers  { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Borrowings)
                .WithOne(b => b.Book)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Membeer>()
                .HasMany(b => b.Borrowings)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Borrowing>()
                .HasIndex(b => new
                {
                    b.MemberId,
                    b.BookId
                }).IsUnique();

            modelBuilder.Entity<Membeer>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .Property(b => b.IsAvailable)
                .HasDefaultValue(true);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();
          
            modelBuilder.Entity<Membeer>()
                .HasData(
                new Membeer { Id = 1 , FullName = "Ahmed Hassan" , Email = "ahmed@example.com" , Phone = "01012345678" } ,
                new Membeer { Id = 2 , FullName = "Sara Mohamed", Email = "sara@example.com" , Phone = "01198765432" }
                );

            modelBuilder.Entity<Category>()
                .HasData(
                new Category { Id = 1 , Name = "Programming" , Description = "Programming and software development books" } ,
                new Category { Id = 2 , Name = "Database", Description = "Database and data management books" }
                );

            modelBuilder.Entity<Book>()
                .HasData(
                new Book { Id = 1 , Title = "C++ Basics" , Author = "John Smith" , ISBN = "ISBN001" , Price = 450 , IsAvailable = true , CategoryId = 1} ,
                new Book { Id = 2 , Title = "SQL Fundamentals", Author = "Sarah Ali", ISBN = "ISBN002", Price = 500 , IsAvailable = false , CategoryId = 2} 
                );

            modelBuilder.Entity<Borrowing>()
                .HasData(
                new Borrowing { Id = 1, BorrowDate = new DateTime(2026 - 09 - 20), ReturnDate = new DateTime(2026 - 09 - 25), BookId = 1, MemberId = 1 },
                new Borrowing { Id = 2, BorrowDate = new DateTime(2026 - 09 - 22), ReturnDate = null, BookId = 2, MemberId = 2 }
                );
        }
    }
}
