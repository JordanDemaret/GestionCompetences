using GestionCompetences.Application.Common.Auth;
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
        private readonly ITokenGenerator _tokenGenerator;

        public UtilisateurService(CompetenceDBContext bdContext, IPasswordHasher passwordHasher, ITokenGenerator token)
        {
            _bdContext = bdContext;
            _passwordHasher = passwordHasher;
            _tokenGenerator = token;
        }

        public Result<Guid> Handle(IncriptionCommande command)
        {
            try
            {
                if (_bdContext.Utilisateurs.Any(u => u.Email == command.Email))
                    return UtilisateurErrors.UtilisateurEmailException;

                Utilisateur utilisateur = new Utilisateur()
                                        { Nom = command.Nom,
                                          Prenom = command.Prenom,
                                          Email = command.Email,
                                          MotDePasse = _passwordHasher.Hash(command.MotDePasse)
                                        };
                _bdContext.Utilisateurs.Add(utilisateur);
                _bdContext.SaveChanges();
                return Result<Guid>.Success(utilisateur.Id);
            }
            catch (Exception)
            {
                return UtilisateurErrors.UtilisateurException;
            }
        }

        public Result<AccessToken> Handle(ConnectionCommande command)
        {
            try
            {
                Utilisateur? utilisateur = _bdContext.Utilisateurs.AsNoTracking()
                                          .SingleOrDefault(u => u.Email == command.Email);
                if (utilisateur is null)
                    return UtilisateurErrors.UtilisationInfoAuthException;

                if (!_passwordHasher.Verify(command.MotDePasse, utilisateur?.MotDePasse))
                    return UtilisateurErrors.UtilisationInfoAuthException;

                return Result<AccessToken>.Success(_tokenGenerator.Generator(utilisateur.Id));
            }
            catch (Exception)
            {
                return UtilisateurErrors.UtilisateurException;
            }
        }
    }
}
