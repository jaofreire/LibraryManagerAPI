using LibraryManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace LibraryManager.Api.Data
{
    public class LibraryMongoDbContext : DbContext
    {
        public LibraryMongoDbContext(DbContextOptions<LibraryMongoDbContext> options) : base(options) { }

        public DbSet<OrderModel> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>().ToCollection("Orders");
            base.OnModelCreating(modelBuilder);
        }

    }
}
