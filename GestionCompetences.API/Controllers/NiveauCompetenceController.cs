using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Entitie.Competence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GestionCompetences.Application.Query;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.API.Dto;
using GestionCompetences.Application.Competence.Commande;

namespace GestionCompetences.API.Controllers
{
    [ApiController]
    [Route("api/niveau")]
    public class NiveauCompetenceController(INiveauCompetenceRepository niveauCompetence): ControllerBase
    {
        private readonly INiveauCompetenceRepository _niveauCompetence = niveauCompetence;

        [HttpGet]
        public IActionResult GetNiveau()
        {
            Result<IEnumerable<NiveauCompetence>> results = _niveauCompetence.Handle(new GetNiveauCompetenceListe());
            if (results.IsFailure)
                return BadRequest(results.Error);

            return Ok(results.Data);
        }
    }
}
