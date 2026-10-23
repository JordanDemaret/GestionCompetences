using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestionCompetences.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/competence")]
    public class CompetenceController(ICompetenceRepository competenceRepository) : ControllerBase
    {

        private readonly ICompetenceRepository _competenceRepository = competenceRepository;

        [HttpGet]
        public IActionResult GetAllCompetenceUtilisateur()
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                return BadRequest("Identifiant utilisateur invalide.");

            Result<IEnumerable<CompetenceDto>> result = _competenceRepository.Handle(new GetCompetenceUtilisateur(userId));
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult AjouterUneCompetence(AddCompetenceDto dto)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                return BadRequest("Identifiant utilisateur invalide.");

            Result<CompetenceDto> result = _competenceRepository.Handle(new AddCompetence(
                dto.Nom,
                dto.Note,
                dto.DateDeDebut,
                dto.Visibilite,
                dto.Statut,
                userId,
                dto.CategorieId,
                dto.NiveauId,
                dto.CertificatId));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpPut]
        public IActionResult ModifierCompetence(ModifierCompetenceDto dto)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                return BadRequest("Identifiant utilisateur invalide.");

            Result result = _competenceRepository.Handle(new ModifierCompetence(
                dto.Id,
                dto.Nom,
                dto.Note,
                dto.DateDeDebut,
                dto.Visibilite,
                dto.Statut,
                userId,
                dto.CategorieId,
                dto.NiveauId,
                dto.CertificatId
                ));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult SupprimerCompetence(Guid id)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                return BadRequest("Identifiant utilisateur invalide.");

            Result result = _competenceRepository.Handle(new DeleteCompetence(id, userId));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
        
    
    }
}
