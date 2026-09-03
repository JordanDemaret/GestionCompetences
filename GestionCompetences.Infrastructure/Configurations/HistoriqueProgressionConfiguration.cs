using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionCompetences.Infrastructure.Configurations
{
    public class HistoriqueProgressionConfiguration : IEntityTypeConfiguration<HistoriqueProgression>
    {
        public void Configure(EntityTypeBuilder<HistoriqueProgression> builder)
        {
            builder.ToTable("Historique_Progression");

            builder.HasOne(hp => hp.Competence)
                   .WithMany(com => com.Historiques)
                   .HasForeignKey(hp => hp.CompetenceId);

            builder.HasOne(hp => hp.AncienNiveau)
                   .WithMany()
                   .HasForeignKey(hp => hp.AncienNiveauId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(hp => hp.NouveauNiveau)
                   .WithMany(niv => niv.Historiques)
                   .HasForeignKey(hp => hp.NouveauNiveauId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
