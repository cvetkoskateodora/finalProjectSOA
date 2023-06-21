using FinalProjectSOA.API.Models.Entities;
using FinalProjectSOA.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectSOA.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        //Database set
        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("users");
        }
    }
}
