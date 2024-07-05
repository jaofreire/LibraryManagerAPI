using Microsoft.EntityFrameworkCore;
using LibraryManager.Core.Models;
using Data.Mapping;

namespace Data.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }

        [DbFunction(name: "SOUNDEX", IsBuiltIn = true)]
        public string FuzzySearch(string queryString)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new AuthorMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
