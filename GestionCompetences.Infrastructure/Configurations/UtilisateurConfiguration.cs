using GestionCompetences.Domain.Enum;
using GestionCompetences.Entitie;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionCompetences.Infrastructure.Configurations
{
    public class UtilisateurConfiguration : IEntityTypeConfiguration<Utilisateur>
    {
        public void Configure(EntityTypeBuilder<Utilisateur> builder)
        {
            builder.ToTable("Utilisateur");

            builder.Property(u => u.Nom)
                   .HasColumnType("NVARCHAR(150)");

            builder.Property(u => u.Prenom)
                   .HasColumnType("NVARCHAR(150)");

            builder.Property(u => u.Email)
                .HasColumnType("NVARCHAR(150)");

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.MotDePasse)
                .HasColumnType("NVARCHAR(500)");

            builder.Property(u => u.EstActif)
                    .HasDefaultValue(true);

            builder.Property(u => u.Role)
                    .HasDefaultValue(Role.Utilisateur);
        }
    }
}
