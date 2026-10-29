using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionCompetences.Infrastructure.Configurations
{
    internal class CategorieConfiguration : IEntityTypeConfiguration<Categorie>
    {
        void IEntityTypeConfiguration<Categorie>.Configure(EntityTypeBuilder<Categorie> builder)
        {
            builder.ToTable("Categorie");

            builder.Property(c => c.Nom)
                   .HasColumnType("NVARCHAR(150)");

            builder.HasIndex(c => c.Nom)
                    .IsUnique();

            builder.HasData(
                new Categorie { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Nom = "Back end"},
                new Categorie { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Nom = "Front end" },
                new Categorie { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), Nom = "C#" },
                new Categorie { Id = Guid.Parse("11111111-1111-1111-1111-111111111114"), Nom = "Angular" },
                new Categorie { Id = Guid.Parse("11111111-1111-1111-1111-111111111115"), Nom = ".Net" }
            );
        }
    }
}
