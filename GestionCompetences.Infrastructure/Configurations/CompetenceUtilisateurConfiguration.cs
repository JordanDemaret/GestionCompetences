using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionCompetences.Infrastructure.Configurations
{
    public class CompetenceUtilisateurConfiguration : IEntityTypeConfiguration<CompetenceUtilisateur>
    {
        public void Configure(EntityTypeBuilder<CompetenceUtilisateur> builder)
        {
            builder.ToTable("Competence_Utilisateur", t =>
            {
                t.HasCheckConstraint("CK_CompetenceU", "Position > 0");
            });

            builder.Property(cu => cu.Nom)
                   .HasColumnType("NVARCHAR(150)");

            builder.Property(cu => cu.Note)
                   .HasColumnType("NVARCHAR(250)");

            builder.Property(cu => cu.Lien)
                   .HasColumnType("NVARCHAR(200)");

            builder.Property(cu => cu.NomGroupe)
                   .HasColumnType("NVARCHAR(150)");

            builder.HasOne(cu => cu.Categorie)
                   .WithMany(ca => ca.CompetencesListe)
                   .HasForeignKey(cu => cu.CategorieId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(cu => cu.Utilisateur)
                   .WithMany(u => u.Competences)
                   .HasForeignKey(cu => cu.UtilisateurId);

            builder.HasOne(cu => cu.CertificatObtenu)
                   .WithMany(cer => cer.CompetencesLsite)
                   .HasForeignKey(cu => cu.CertificatId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cu => cu.Niveau)
                   .WithMany(ca => ca.CompetenceListe)
                   .HasForeignKey(cu => cu.NiveauId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
