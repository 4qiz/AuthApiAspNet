using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BestAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace BestAuth.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().Property(u => u.Name).HasMaxLength(256);
        }
    
    }
}
