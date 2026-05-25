
using BestAuth.Domain.Entities;
using BestAuth.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BestAuth.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Identity
            builder.Services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 1;

                o.User.RequireUniqueEmail = true; // false for only username auth
                o.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            // DbContext
            var connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? ""; // throw error
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connection));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
