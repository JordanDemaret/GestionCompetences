using EFGestionCompetences.Entitie;
using Microsoft.EntityFrameworkCore;

namespace EFGestionCompetences
{
    public class CompetenceDBContext : DbContext  
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
