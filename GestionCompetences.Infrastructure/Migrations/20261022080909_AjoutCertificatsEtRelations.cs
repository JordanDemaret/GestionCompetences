using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCompetences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjoutCertificatsEtRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competence_Utilisateur_Certificat_obtenue_CertificatId",
                table: "Competence_Utilisateur");

            migrationBuilder.AddForeignKey(
                name: "FK_Competence_Utilisateur_Certificat_obtenue_CertificatId",
                table: "Competence_Utilisateur",
                column: "CertificatId",
                principalTable: "Certificat_obtenue",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competence_Utilisateur_Certificat_obtenue_CertificatId",
                table: "Competence_Utilisateur");

            migrationBuilder.AddForeignKey(
                name: "FK_Competence_Utilisateur_Certificat_obtenue_CertificatId",
                table: "Competence_Utilisateur",
                column: "CertificatId",
                principalTable: "Certificat_obtenue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
