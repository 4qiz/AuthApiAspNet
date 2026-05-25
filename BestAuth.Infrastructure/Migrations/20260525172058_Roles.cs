using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BestAuth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("70319e53-4ab9-44c0-8a4f-d933f2eb8f2c"), "70319e53-4ab9-44c0-8a4f-d933f2eb8f2c", "User", "USER" },
                    { new Guid("9ca1d0b0-8f70-4cb2-9c60-6db2d8c45317"), "9ca1d0b0-8f70-4cb2-9c60-6db2d8c45317", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("70319e53-4ab9-44c0-8a4f-d933f2eb8f2c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ca1d0b0-8f70-4cb2-9c60-6db2d8c45317"));
        }
    }
}
