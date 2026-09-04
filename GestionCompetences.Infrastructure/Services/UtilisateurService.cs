using GestionCompetences.Application.Common.Hasher;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Utilisateur;
using GestionCompetences.Application.Utilisateur.Commande;
using GestionCompetences.Entitie;
using Microsoft.EntityFrameworkCore;

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

        public Result Handle(IncriptionCommande command)
        {
            try
            {
                if(_bdContext.Utilisateurs.Any(u => u.Email == command.Email))
                    return Error.Create("Utilisateur.Email", "L'email existe déjà.");


                _bdContext.Utilisateurs.Add(new Utilisateur()
                {
                    Nom = command.Nom,
                    Prenom = command.Prenom,
                    Email = command.Email,
                    MotDePasse = _passwordHasher.Hash(command.MotDePasse)
                });
                _bdContext.SaveChanges();
                return Result.Success();
            }
            catch (Exception)
            {
                return Error.Create("Utilisateur.Exception", "Une exception est survenue.");
            }
        }

        public Result Handle(ConnectionCommande command)
        {
            try
            {
                Utilisateur? utilisateur = _bdContext.Utilisateurs.AsNoTracking()
                                          .SingleOrDefault(u => u.Email == command.Email);

                if (utilisateur is null)
                    return Error.Create("Utilisateur.MotDePassError", "Email ou mot des passe incorrect.");

                if (! _passwordHasher.Verify(command.MotDePasse, utilisateur?.MotDePasse))
                    return Error.Create("Utilisateur.MotDePassError", "Email ou mot des passe incorrect.");
                return Result.Success();
            }
            catch (Exception)
            {
                return Error.Create("Utilisateur.Exception", "Une exception est survenue.");
            }
        }
    }
}
