using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestionCompetences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategorieDefaut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorie",
                columns: new[] { "Id", "Nom" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Back end" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), "Front end" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), "C#" },
                    { new Guid("11111111-1111-1111-1111-111111111114"), "Angular" },
                    { new Guid("11111111-1111-1111-1111-111111111115"), ".Net" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"));

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"));

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"));
        }
    }
}
