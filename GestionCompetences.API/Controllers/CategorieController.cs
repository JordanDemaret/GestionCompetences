using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Query;
using GestionCompetences.Entitie.Competence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompetences.API.Controllers
{
    [ApiController]
    [Route("api/competence")]
    public class CategorieController(ICategorieRepository categorieRepository) : ControllerBase
    {
        private readonly ICategorieRepository _categorieRepository = categorieRepository;

        [HttpGet]
        public IActionResult getCategorie()
        {
            Result<IEnumerable<Categorie>> result = _categorieRepository.Handle(new GetCategorieListe());

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult addCoategorie(AddCompetence addCompetence)
        {
            Result result = _categorieRepository.Handle(new AddCompetence(addCompetence.Nom));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }
    }
}
