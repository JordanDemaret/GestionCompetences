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
        }
    }
}
