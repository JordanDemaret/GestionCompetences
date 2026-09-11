using GestionCompetences.Entitie;
using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;

namespace GestionCompetences.Infrastructure
{
    public class CompetenceDBContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get { return Set<Utilisateur>(); } }
        public DbSet<Categorie> Categories { get { return Set<Categorie>(); } }
        public DbSet<CertificatObtenu> Certificats { get { return Set<CertificatObtenu>(); } }
        public DbSet<CompetenceUtilisateur> Competences { get { return Set<CompetenceUtilisateur>(); } }
        public DbSet<HistoriqueProgression> Historiques { get { return Set<HistoriqueProgression>(); } }
        public DbSet<NiveauCompetence> Niveau { get { return Set<NiveauCompetence>(); } }

        public CompetenceDBContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompetenceDBContext).Assembly);
        }
    }
}
