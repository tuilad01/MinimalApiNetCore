using Microsoft.EntityFrameworkCore;
using Services.Dictionary;
using Services.Todo;

namespace MinimalApiNetCore.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Dictionary> Dicitionaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Todo");
            modelBuilder.Entity<Dictionary>().ToTable("Dictionary");
        }

    }
}
