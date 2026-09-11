using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Query;
using GestionCompetences.Entitie.Competence;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GetCategorie()
        {
            Result<IEnumerable<Categorie>> result = _categorieRepository.Handle(new GetCategorieListe());

            return Ok(result.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategorie(AddCategorie add)
        {
            Result result = _categorieRepository.Handle(new AddCategorie(add.Nom));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult ModiffierCategorie(ModifierCategorieDto modifier)
        {
            Result result = _categorieRepository.Handle(new ModifierCategorie(
                                                                    modifier.Id, 
                                                                    modifier.Nom));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }
    }
}
