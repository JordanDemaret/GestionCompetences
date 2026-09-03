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

            builder.Property(n => n.CompetenceListe)
                   .HasColumnType("NVARCHAR(200)");
        }
    }
}
