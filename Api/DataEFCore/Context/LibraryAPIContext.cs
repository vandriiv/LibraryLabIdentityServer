using System;
using DataEFCore.Configurations;
using DataEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataEFCore.Context
{
    public partial class LibraryAPIContext : DbContext
    {
        public LibraryAPIContext()
        {
        }

        public LibraryAPIContext(DbContextOptions<LibraryAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserBook> UserBook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            new AuthorConfiguration(modelBuilder.Entity<Author>());
            new BookConfiguration(modelBuilder.Entity<Book>());
            new GenreConfiguration(modelBuilder.Entity<Genre>());           
            new UserBookConfiguration(modelBuilder.Entity<UserBook>());
            new UserConfiguration(modelBuilder.Entity<User>());
        }
    }
}
