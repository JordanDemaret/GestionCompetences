using GestionCompetences.Application.form;
using GestionCompetences.Application.Utilisateur;
using GestionCompetences.Application.Utilisateur.Commande;
using Microsoft.AspNetCore.Mvc;


namespace GestionCompetences.API.Controllers
{
    [ApiController]
    [Route("api/utilisateur")]
    public class UtilisateurController(IUtilisateurRepository utilisateurRepository) : ControllerBase
    {
        private readonly IUtilisateurRepository _utilisateurRepository = utilisateurRepository;

        [HttpPost]
        public IActionResult Inscription(InscriptionDto inscription)
        {
            _utilisateurRepository.Handle(new IncriptionCommande(inscription.Nom, inscription.Prenom, inscription.Email, inscription.MotDePasse));
            return Ok();
        }
    }
}
