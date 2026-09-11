using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Auth;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.form;
using GestionCompetences.Application.Utilisateur;
using GestionCompetences.Application.Utilisateur.Commande;
using Microsoft.AspNetCore.Mvc;


namespace GestionCompetences.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IUtilisateurRepository utilisateurRepository) : ControllerBase
    {
        private readonly IUtilisateurRepository _utilisateurRepository = utilisateurRepository;

        [HttpPost]
        public IActionResult Inscription(InscriptionDto inscription)
        {
            Result<Guid> result = _utilisateurRepository.Handle(new IncriptionCommande(inscription.Nom, inscription.Prenom, inscription.Email, inscription.MotDePasse));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpPost("login")]
        public ActionResult<AccessTokenDto> Connection(ConnectionDto connection)
        {
            Result<AccessToken> result = _utilisateurRepository.Handle(new ConnexionCommande(connection.Email, connection.MotDePasse));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(AccessTokenDto.From(result.Data));
        }
    }
}
