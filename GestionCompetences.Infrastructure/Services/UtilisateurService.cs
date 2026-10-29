using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Auth;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Dto;
using GestionCompetences.Application.Utilisateur;
using GestionCompetences.Application.Utilisateur.Commande;
using GestionCompetences.Entitie;

namespace GestionCompetences.Infrastructure.Services
{
    /// <summary>
    /// Service d'infrastructure implémentant la gestion des utilisateurs et le flux d'authentification.
    /// </summary>
    public class UtilisateurService : IUtilisateurRepository
    {
        private readonly CompetenceDBContext  _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="UtilisateurService"/>.
        /// </summary>
        /// <param name="dbContext">Le contexte de base de données.</param>
        /// <param name="passwordHasher">Le service de hachage de mot de passe.</param>
        /// <param name="token">Le générateur de jetons JWT et Refresh Tokens.</param>
        public UtilisateurService(CompetenceDBContext dbContext, IPasswordHasher passwordHasher, ITokenGenerator token)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _tokenGenerator = token;
        }

        /// <summary>
        /// Inscrit un nouvel utilisateur en hachant son mot de passe et en vérifiant l'unicité de son adresse email.
        /// </summary>
        /// <param name="command">La commande contenant le nom, prénom, email et mot de passe en clair.</param>
        /// <returns>L'identifiant unique (GUID) de l'utilisateur créé ou une erreur.</returns>
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

        /// <summary>
        /// Authentifie l'utilisateur à partir de ses identifiants, vérifie son mot de passe et génère un jeton JWT avec un nouveau Refresh Token.
        /// </summary>
        /// <param name="command">La commande contenant l'adresse email et le mot de passe en clair.</param>
        /// <returns>Le profil de l'utilisateur avec son jeton d'accès et son jeton de rafraîchissement ou une erreur d'authentification.</returns>
        public Result<UtilisateurDto> Handle(ConnexionCommande command)
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

                return Result<UtilisateurDto>.Success(UtilisateurDto.From(utilisateur, token));
            }
            catch (Exception)
            {
                return UtilisateurErrors.UtilisateurException;
            }
        }

        /// <summary>
        /// Valide le Refresh Token transmis et génère une nouvelle paire de jetons d'accès et de rafraîchissement.
        /// </summary>
        /// <param name="command">La commande contenant le jeton expiré et le Refresh Token.</param>
        /// <returns>La nouvelle paire de jetons ou une erreur si le jeton est invalide ou l'utilisateur non trouvé.</returns>
        public Result<PairTokenDto> Handle(RefreshTokenCommande command)
        {
            try
            {
                Guid guid = _tokenGenerator.GetUserTokenId(command.Token);

                Utilisateur? utilisateur = _dbContext.Utilisateurs.SingleOrDefault(u => u.Id == guid);

                if (utilisateur is null)
                    return Result<PairTokenDto>.Failure(UtilisateurErrors.UtilisateurIntrouvableException);
                 
                if (utilisateur.RefreshToken != command.RefreshToken)
                    return Result<PairTokenDto>.Failure(UtilisateurErrors.UtilisateurRefreshException);

                string token = _tokenGenerator.Generator(utilisateur!);
                utilisateur!.RefreshToken = _tokenGenerator.GenerateRefreshToken();

                _dbContext.SaveChanges();

                return Result<PairTokenDto>.Success(PairTokenDto.From(token, utilisateur.RefreshToken));
            }
            catch
            {
                return Result<PairTokenDto>.Failure(UtilisateurErrors.UtilisateurException);

            }
        }
    }
}
