using GestionCompetences.Entitie;
using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;

namespace GestionCompetences.Infrastructure
{
    /// <summary>
    /// Contexte Entity Framework Core pour la gestion des compétences et de la base de données de l'application.
    /// </summary>
    public class CompetenceDBContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get { return Set<Utilisateur>(); } }
        public DbSet<Categorie> Categories { get { return Set<Categorie>(); } }
        public DbSet<CertificatObtenu> Certificats { get { return Set<CertificatObtenu>(); } }
        public DbSet<CompetenceUtilisateur> Competences { get { return Set<CompetenceUtilisateur>(); } }
        public DbSet<HistoriqueProgression> Historiques { get { return Set<HistoriqueProgression>(); } }
        public DbSet<NiveauCompetence> Niveau { get { return Set<NiveauCompetence>(); } }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="CompetenceDBContext"/> avec les options spécifiées.
        /// </summary>
        /// <param name="options">Les options de configuration du contexte.</param>
        public CompetenceDBContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompetenceDBContext).Assembly);
        }
    }
}
