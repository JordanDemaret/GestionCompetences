using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCompetences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class corresction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MotDePasse",
                table: "Utilisateur",
                type: "NVARCHAR(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(200)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MotDePasse",
                table: "Utilisateur",
                type: "NVARCHAR(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(300)");
        }
    }
}
