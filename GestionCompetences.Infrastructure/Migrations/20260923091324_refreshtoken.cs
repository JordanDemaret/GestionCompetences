using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCompetences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MotDePasse",
                table: "Utilisateur",
                type: "NVARCHAR(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(500)");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Utilisateur",
                type: "NVARCHAR(200)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Utilisateur");

            migrationBuilder.AlterColumn<string>(
                name: "MotDePasse",
                table: "Utilisateur",
                type: "NVARCHAR(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(200)");
        }
    }
}
