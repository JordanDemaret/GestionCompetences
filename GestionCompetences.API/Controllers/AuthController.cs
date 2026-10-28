using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Dto;
using GestionCompetences.Application.form;
using GestionCompetences.Application.Utilisateur;
using GestionCompetences.Application.Utilisateur.Commande;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompetences.API.Controllers
{
    /// <summary>
    /// Contrôleur gérant l'authentification et l'inscription des utilisateurs.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IUtilisateurRepository utilisateurRepository) : ControllerBase
    {
        private readonly IUtilisateurRepository _utilisateurRepository = utilisateurRepository;

        /// <summary>
        /// Inscrit un nouvel utilisateur dans l'application.
        /// </summary>
        /// <param name="inscription">Les informations d'inscription de l'utilisateur.</param>
        /// <returns>L'identifiant unique de l'utilisateur créé ou une erreur.</returns>
        [HttpPost]
        public IActionResult Inscription(InscriptionDto inscription)
        {
            Result<Guid> result = _utilisateurRepository.Handle(new IncriptionCommande(inscription.Nom, inscription.Prenom, inscription.Email, inscription.MotDePasse));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        /// <summary>
        /// Authentifie un utilisateur à l'aide de son adresse email et de son mot de passe.
        /// </summary>
        /// <param name="connection">Les identifiants de connexion de l'utilisateur.</param>
        /// <returns>Les informations de profil et les jetons d'accès et de rafraîchissement.</returns>
        [HttpPost("login")]
        public IActionResult Connection(ConnectionDto connection)
        {
            Result<UtilisateurDto> result = _utilisateurRepository.Handle(new ConnexionCommande(connection.Email, connection.MotDePasse));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        /// <summary>
        /// Renouvelle le jeton d'authentification expiré à l'aide d'un jeton de rafraîchissement valide.
        /// </summary>
        /// <param name="refeshe">Les informations nécessaires au rafraîchissement des jetons.</param>
        /// <returns>La nouvelle paire de jetons d'accès et de rafraîchissement.</returns>
        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenDto refeshe)
        {
            Result<PairTokenDto> result = _utilisateurRepository.Handle(new RefreshTokenCommande(refeshe.Token, refeshe.RefreshToken));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }
    }
}
