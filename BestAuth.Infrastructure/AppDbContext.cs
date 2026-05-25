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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().Property(u => u.Name).HasMaxLength(256);

            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("9ca1d0b0-8f70-4cb2-9c60-6db2d8c45317"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "9ca1d0b0-8f70-4cb2-9c60-6db2d8c45317"
                },
                new Role
                {
                    Id = Guid.Parse("70319e53-4ab9-44c0-8a4f-d933f2eb8f2c"),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "70319e53-4ab9-44c0-8a4f-d933f2eb8f2c"
                }
            );
        }
    
    }
}
