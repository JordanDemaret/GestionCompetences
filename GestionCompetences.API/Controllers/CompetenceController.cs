using GestionCompetences.API.Dto;
using GestionCompetences.API.Extention;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            Guid? userId = User.GetUserId();
            if (userId is null)
                return BadRequest("Identifiant utilisateur invalide.");

            Result<IEnumerable<CompetenceDto>> result = _competenceRepository.Handle(new GetCompetenceUtilisateur(userId.Value));
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult AjouterUneCompetence(AddCompetenceDto dto)
        {

            Guid? userId = User.GetUserId();
            if (userId is null)
                return BadRequest("Identifiant utilisateur invalide.");

            Result<CompetenceDto> result = _competenceRepository.Handle(new AddCompetence(
                dto.Nom,
                dto.Note,
                dto.DateDeDebut,
                dto.Visibilite,
                dto.Statut,
                userId.Value,
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

            Guid? userId = User.GetUserId();
            if (userId is null)
                return BadRequest("Identifiant utilisateur invalide.");

            Result result = _competenceRepository.Handle(new ModifierCompetence(
                dto.Id,
                dto.Nom,
                dto.Note,
                dto.DateDeDebut,
                dto.Visibilite,
                dto.Statut,
                userId.Value,
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

            Guid? userId = User.GetUserId();
            if (userId is null)
                return BadRequest("Identifiant utilisateur invalide.");

            Result result = _competenceRepository.Handle(new DeleteCompetence(id, userId.Value));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut("repositionner")]
        public IActionResult RepositionnerCompetences([FromBody] MiseAJourPossitionDto dto)
        {

            Guid? userId = User.GetUserId();
            if (userId is null)
                return BadRequest("Identifiant utilisateur invalide.");

            Result result = _competenceRepository.Handle(new MiseAJourPossition(dto.IdCompetences, userId.Value));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
