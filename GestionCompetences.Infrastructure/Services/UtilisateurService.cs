using GestionCompetences.Application.Common.Hasher;
using GestionCompetences.Application.Utilisateur;
using GestionCompetences.Application.Utilisateur.Commande;
using GestionCompetences.Entitie;

namespace GestionCompetences.Infrastructure.Services
{
    public class UtilisateurService : IUtilisateurRepository
    {
        private readonly CompetenceDBContext  _bdContext;
        private readonly IPasswordHasher _passwordHasher;

        public UtilisateurService(CompetenceDBContext bdContext, IPasswordHasher passwordHasher)
        {
            _bdContext = bdContext;
            _passwordHasher = passwordHasher;
        }

        public void Handle(IncriptionCommande command)
        {
            _bdContext.Set<Utilisateur>().Add(new Utilisateur()
            {
                Nom = command.Nom,
                Prenom = command.Prenom,
                Email = command.Email,
                MotDePasse = _passwordHasher.Hash(command.MaoDePassHaser)
            });
            _bdContext.SaveChanges();
        }
    }
}
