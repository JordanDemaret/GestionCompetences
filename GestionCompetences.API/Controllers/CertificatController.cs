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
    [ApiController]
    [Authorize]
    [Route("api/certificat")]
    public class CertificatController(ICertificatRepository certificatRepository) : ControllerBase
    {
        private readonly ICertificatRepository _certificatRepository = certificatRepository;

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
