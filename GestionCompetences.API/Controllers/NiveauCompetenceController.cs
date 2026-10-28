using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Entitie.Competence;
using Microsoft.AspNetCore.Mvc;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Common.Results;
using Microsoft.AspNetCore.Authorization;

namespace GestionCompetences.API.Controllers
{
    /// <summary>
    /// Contrôleur permettant d'obtenir la liste des niveaux de compétences.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/niveau")]
    public class NiveauCompetenceController(INiveauCompetenceRepository niveauCompetence): ControllerBase
    {
        private readonly INiveauCompetenceRepository _niveauCompetence = niveauCompetence;

        /// <summary>
        /// Récupère la liste de tous les niveaux de compétences disponibles.
        /// </summary>
        /// <returns>La liste des niveaux de compétences ou une erreur.</returns>
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
