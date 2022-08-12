using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchApi.Persistence.Migrations
{
    public partial class SeedingAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreateAt", "CreatedBy", "Email", "Name", "Phone", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "limabrot879@gmail.com", "ADMIN", "+55 (81) 93618-4134", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
