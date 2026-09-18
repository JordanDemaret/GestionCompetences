using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestionCompetences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataNiveau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Niveau_competence",
                keyColumn: "Id",
                keyValue: new Guid("3db6141a-e499-4081-a454-2aa132d4bce8"));

            migrationBuilder.DeleteData(
                table: "Niveau_competence",
                keyColumn: "Id",
                keyValue: new Guid("595681b2-d5aa-40d5-a1dd-6aff09380664"));

            migrationBuilder.DeleteData(
                table: "Niveau_competence",
                keyColumn: "Id",
                keyValue: new Guid("c9c996d7-342f-4546-8499-bb91a5da046b"));

            migrationBuilder.DeleteData(
                table: "Niveau_competence",
                keyColumn: "Id",
                keyValue: new Guid("d79a4a17-4e40-40e2-898c-a6c99b751eb6"));

            migrationBuilder.InsertData(
                table: "Niveau_competence",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Débutant" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Intermédiaire" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Avancé" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Expert" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Niveau_competence",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Niveau_competence",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Niveau_competence",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Niveau_competence",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.InsertData(
                table: "Niveau_competence",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { new Guid("3db6141a-e499-4081-a454-2aa132d4bce8"), "Débutant" },
                    { new Guid("595681b2-d5aa-40d5-a1dd-6aff09380664"), "Avancé" },
                    { new Guid("c9c996d7-342f-4546-8499-bb91a5da046b"), "Intermédiaire" },
                    { new Guid("d79a4a17-4e40-40e2-898c-a6c99b751eb6"), "Expert" }
                });
        }
    }
}
