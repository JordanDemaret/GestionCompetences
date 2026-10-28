using GestionCompetences.API.Dto;
using GestionCompetences.API.Extention;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Dto;
using GestionCompetences.Entitie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestionCompetences.API.Controllers
{
    /// <summary>
    /// Contrôleur gérant les certificats des utilisateurs connectés.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/certificat")]
    public class CertificatController(ICertificatRepository certificatRepository) : ControllerBase
    {
        private readonly ICertificatRepository _certificatRepository = certificatRepository;

        /// <summary>
        /// Récupère l'ensemble des certificats appartenant à l'utilisateur connecté.
        /// </summary>
        /// <returns>La liste des certificats sous forme de DTO ou une erreur.</returns>
        [HttpGet]
        public IActionResult GetCertificat()
        {
            Guid? utilisateurId = User.GetUserId();
            if (utilisateurId is null)
                return BadRequest("Identifiant utilisateur invalide.");

            Result<IEnumerable<CertificatDto>> result = _certificatRepository.Handle(new GetCertificatListe(utilisateurId.Value));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        /// <summary>
        /// Ajoute un nouveau certificat pour l'utilisateur connecté.
        /// </summary>
        /// <param name="dto">Les informations du certificat à enregistrer.</param>
        /// <returns>Le certificat créé ou une erreur.</returns>
        [HttpPost]
        public IActionResult AjouterCertificat(AddCertificatDto dto)
        {


            Guid? utilisateurId = User.GetUserId();
            if (utilisateurId is null)
                return BadRequest("Identifiant utilisateur invalide.");

            Result<CertificatDto> result = _certificatRepository.Handle(new AddCertificat(
                                                                dto.NomCertificat,
                                                                dto.CodeCertificat,
                                                                dto.Editeur,
                                                                dto.DateObtention,
                                                                utilisateurId.Value
                                                                ));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        /// <summary>
        /// Modifie un certificat existant appartenant à l'utilisateur connecté.
        /// </summary>
        /// <param name="dto">Les nouvelles données du certificat.</param>
        /// <returns>Un résultat vide confirmant la mise à jour ou une erreur si le certificat est introuvable.</returns>
        [HttpPut]
        public IActionResult ModifierCertificat(ModifierCertificatDto dto)
        {

            Guid? utilisateurId = User.GetUserId();
            if (utilisateurId is null)
                return BadRequest("Identifiant utilisateur invalide.");

            Result result = _certificatRepository.Handle(new ModifierCertificat(
                                                                dto.Id,
                                                                dto.NomCertificat,
                                                                dto.CodeCertificat,
                                                                dto.Editeur,
                                                                dto.DateObtention,
                                                                utilisateurId.Value
                                                                ));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }

        /// <summary>
        /// Supprime un certificat appartenant à l'utilisateur connecté.
        /// </summary>
        /// <param name="id">L'identifiant unique du certificat à supprimer.</param>
        /// <returns>Un résultat vide confirmant la suppression ou une erreur si le certificat n'existe pas.</returns>
        [HttpDelete("{id:Guid}")]
        public IActionResult SuppressionCertificat(Guid id)
        {

            Guid? utilisateurId = User.GetUserId();
            if (utilisateurId is null) 
                return BadRequest("Identifiant utilisateur invalide.");


            Result result = _certificatRepository.Handle(new DeleteCertificat(id, utilisateurId.Value));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }

}
