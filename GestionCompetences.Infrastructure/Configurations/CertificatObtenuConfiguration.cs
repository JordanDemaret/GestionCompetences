using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionCompetences.Infrastructure.Configurations
{
    public class CertificatObtenuConfiguration : IEntityTypeConfiguration<CertificatObtenu>
    {
        public void Configure(EntityTypeBuilder<CertificatObtenu> builder)
        {
            builder.ToTable("Certificat_obtenue", t =>
                    t.HasCheckConstraint("CK_CertificatObtenue_Date", "YEAR(DateObtention) >= 2000")
            );

            builder.Property(c => c.NomCertificat)
                   .HasColumnType("NVARCHAR(150)");

            builder.Property(c => c.CodeCertificat)
                   .HasColumnType("NVARCHAR(250)");

            builder.Property(c => c.Editeur)
                   .HasColumnType("NVARCHAR(200)");

            builder.HasOne(c => c.Utilisateur)
                   .WithMany(u => u.CertificatsObtenus)
                   .HasForeignKey(c => c.UtilisateurId);

        }
    }
}
