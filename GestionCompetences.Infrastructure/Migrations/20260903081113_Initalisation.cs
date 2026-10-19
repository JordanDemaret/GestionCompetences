using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCompetences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initalisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "NVARCHAR(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niveau_competence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "NVARCHAR(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau_competence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    Prenom = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    MotDePasse = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    EstActif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificat_obtenue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomCertificat = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    CodeCertificat = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Editeur = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    DateObtention = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UtilisateurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificat_obtenue", x => x.Id);
                    table.CheckConstraint("CK_CertificatObtenue_Date", "YEAR(DateObtention) >= 2000");
                    table.ForeignKey(
                        name: "FK_Certificat_obtenue_Utilisateur_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competence_Utilisateur",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    Visibilite = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    Lien = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    DateDeDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    NomGroupe = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    CategorieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UtilisateurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CertificatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NiveauId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competence_Utilisateur", x => x.Id);
                    table.CheckConstraint("CK_CompetenceU", "Position > 0");
                    table.ForeignKey(
                        name: "FK_Competence_Utilisateur_Categorie_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competence_Utilisateur_Certificat_obtenue_CertificatId",
                        column: x => x.CertificatId,
                        principalTable: "Certificat_obtenue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competence_Utilisateur_Niveau_competence_NiveauId",
                        column: x => x.NiveauId,
                        principalTable: "Niveau_competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competence_Utilisateur_Utilisateur_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historique_Progression",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompetenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AncienNiveauId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NouveauNiveauId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historique_Progression", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historique_Progression_Competence_Utilisateur_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence_Utilisateur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historique_Progression_Niveau_competence_AncienNiveauId",
                        column: x => x.AncienNiveauId,
                        principalTable: "Niveau_competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Historique_Progression_Niveau_competence_NouveauNiveauId",
                        column: x => x.NouveauNiveauId,
                        principalTable: "Niveau_competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorie_Nom",
                table: "Categorie",
                column: "Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificat_obtenue_UtilisateurId",
                table: "Certificat_obtenue",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Competence_Utilisateur_CategorieId",
                table: "Competence_Utilisateur",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Competence_Utilisateur_CertificatId",
                table: "Competence_Utilisateur",
                column: "CertificatId");

            migrationBuilder.CreateIndex(
                name: "IX_Competence_Utilisateur_NiveauId",
                table: "Competence_Utilisateur",
                column: "NiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_Competence_Utilisateur_UtilisateurId",
                table: "Competence_Utilisateur",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Historique_Progression_AncienNiveauId",
                table: "Historique_Progression",
                column: "AncienNiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_Historique_Progression_CompetenceId",
                table: "Historique_Progression",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Historique_Progression_NouveauNiveauId",
                table: "Historique_Progression",
                column: "NouveauNiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_Niveau_competence_Label",
                table: "Niveau_competence",
                column: "Label",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_Email",
                table: "Utilisateur",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historique_Progression");

            migrationBuilder.DropTable(
                name: "Competence_Utilisateur");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "Certificat_obtenue");

            migrationBuilder.DropTable(
                name: "Niveau_competence");

            migrationBuilder.DropTable(
                name: "Utilisateur");
        }
    }
}
