using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionCompetences.Infrastructure.Configurations
{
    partial class NiveauCompetenceConfiguration : IEntityTypeConfiguration<NiveauCompetence>
    {
        public void Configure(EntityTypeBuilder<NiveauCompetence> builder)
        {
            builder.ToTable("Niveau_competence");

            builder.Property(n => n.Label)
                   .HasColumnType("NVARCHAR(200)");

            builder.HasIndex(n => n.Label)
                .IsUnique();
        }
    }
}
