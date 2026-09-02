using EFGestionCompetences.Entitie;
using EFGestionCompetences.Entitie.Compétence;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace EFGestionCompetences
{
    public class CompetenceDBContext : DbContext  
    {
        public DbSet<Utilisateur> Utilisateurs { get { return Set<Utilisateur>(); } }
        public DbSet<Categorie> Categories { get { return Set<Categorie>(); } }
        public DbSet<Certificat> Certificats { get { return Set<Certificat>(); } }
        public DbSet<CompetenceUtilisateur> competenceUtilisateurs { get { return Set<CompetenceUtilisateur>(); } }

        public CompetenceDBContext(DbContextOptions options) : base(options) { }
    
    }
}
