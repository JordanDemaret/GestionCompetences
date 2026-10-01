using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Entitie.Competence;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompetences.API.Controllers
{
    [ApiController]
    [Route("api/competence")]
    public class CompetenceController(ICompetenceRepository competenceRepository) : ControllerBase
    {

        private readonly ICompetenceRepository _competenceRepository = competenceRepository;

        [HttpGet("{guid:Guid}")]
        public IActionResult GatAllCompetenceUtilisateur(Guid guid)
        {
            Result<IEnumerable<CompetenceDto>> result = _competenceRepository.Handle(new GetCompetenceUtilisateur(guid));
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult AjouterUneCompetence(AddCompetenceDto dto)
        {
            Result result = _competenceRepository.Handle(new AddCompetence(dto.Nom, dto.Note, dto.DateDeDebut, dto.Visibilite, dto.Statut, dto.Position, dto.CategorieId, dto.UtilisateurId, dto.NiveauId));

            if (result.IsFailure)
                return BadRequest(result.Error);
            
            return Ok();
        }
    }
}
