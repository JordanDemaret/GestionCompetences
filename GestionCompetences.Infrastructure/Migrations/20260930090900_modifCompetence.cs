using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCompetences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifCompetence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lien",
                table: "Competence_Utilisateur");

            migrationBuilder.DropColumn(
                name: "NomGroupe",
                table: "Competence_Utilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lien",
                table: "Competence_Utilisateur",
                type: "NVARCHAR(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomGroupe",
                table: "Competence_Utilisateur",
                type: "NVARCHAR(150)",
                nullable: false,
                defaultValue: "");
        }
    }
}
