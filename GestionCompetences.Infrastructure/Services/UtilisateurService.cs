using GestionCompetences.Application.Common.Auth;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Utilisateur;
using GestionCompetences.Application.Utilisateur.Commande;
using GestionCompetences.Entitie;

namespace GestionCompetences.Infrastructure.Services
{
    public class UtilisateurService : IUtilisateurRepository
    {
        private readonly CompetenceDBContext  _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public UtilisateurService(CompetenceDBContext dbContext, IPasswordHasher passwordHasher, ITokenGenerator token)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _tokenGenerator = token;
        }

        public Result<Guid> Handle(IncriptionCommande command)
        {
            try
            {
                if (_dbContext.Utilisateurs.Any(u => u.Email == command.Email))
                    return UtilisateurErrors.UtilisateurEmailException;

                Utilisateur utilisateur = new Utilisateur()
                                        { Nom = command.Nom,
                                          Prenom = command.Prenom,
                                          Email = command.Email,
                                          MotDePasse = _passwordHasher.Hash(command.MotDePasse)
                                        };
                _dbContext.Utilisateurs.Add(utilisateur);
                _dbContext.SaveChanges();
                return Result<Guid>.Success(utilisateur.Id);
            }
            catch (Exception)
            {
                return UtilisateurErrors.UtilisateurException;
            }
        }

        public Result<InfoUtiisateur> Handle(ConnexionCommande command)
        {
            try
            {
                Utilisateur? utilisateur = _dbContext.Utilisateurs.SingleOrDefault(u => u.Email == command.Email);
                if (utilisateur is null)
                    return UtilisateurErrors.UtilisationInfoAuthException;

                if (!_passwordHasher.Verify(command.MotDePasse, utilisateur?.MotDePasse))
                    return UtilisateurErrors.UtilisationInfoAuthException;

                string token = _tokenGenerator.Generator(utilisateur!);
                utilisateur!.RefreshToken = _tokenGenerator.GenerateRefreshToken();

                _dbContext.SaveChanges();

                return Result<InfoUtiisateur>.Success(new InfoUtiisateur(token, utilisateur));
            }
            catch (Exception)
            {
                return UtilisateurErrors.UtilisateurException;
            }
        }

        public Result<PairToken> Handle(RefreshTokenCommande command)
        {
            try
            {
                Guid guid = _tokenGenerator.GetUserTokenId(command.Token);

                Utilisateur? utilisateur = _dbContext.Utilisateurs.SingleOrDefault(u => u.Id == guid);

                if (utilisateur is null)
                    return Result<PairToken>.Failure(UtilisateurErrors.UtilisateurIntrouvableException);
                 
                if (utilisateur.RefreshToken != command.RefreshToken)
                    return Result<PairToken>.Failure(UtilisateurErrors.UtilisateurRefreshException);

                string token = _tokenGenerator.Generator(utilisateur!);
                utilisateur!.RefreshToken = _tokenGenerator.GenerateRefreshToken();

                _dbContext.SaveChanges();

                return Result<PairToken>.Success(new PairToken(token, utilisateur.RefreshToken));
            }
            catch
            {
                return Result<PairToken>.Failure(UtilisateurErrors.UtilisateurException);

            }
        }
    }
}
