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

            builder.HasData(
                new NiveauCompetence { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Label = "Débutant" },
                new NiveauCompetence { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Label = "Intermédiaire" },
                new NiveauCompetence { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Label = "Avancé" },
                new NiveauCompetence { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Label = "Expert" }
            );

        }
    }
}
