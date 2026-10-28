using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Query;
using GestionCompetences.Domain.Enum;
using GestionCompetences.Entitie.Competence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompetences.API.Controllers
{
    /// <summary>
    /// Contrôleur gérant les catégories de compétences.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/categorie")]
    public class CategorieController(ICategorieRepository categorieRepository) : ControllerBase
    {
        private readonly ICategorieRepository _categorieRepository = categorieRepository;

        /// <summary>
        /// Récupère la liste de toutes les catégories de compétences disponibles.
        /// </summary>
        /// <returns>La liste des catégories de compétences.</returns>
        [HttpGet]
        public IActionResult GetCategorie()
        {
            Result<IEnumerable<Categorie>> result = _categorieRepository.Handle(new GetCategorieListe());

            return Ok(result.Data);
        }

        /// <summary>
        /// Ajoute une nouvelle catégorie de compétences.
        /// </summary>
        /// <param name="add">Les données de la catégorie à créer.</param>
        /// <returns>La catégorie créée ou une erreur si elle existe déjà.</returns>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public IActionResult AddCategorie(AddCategorieDto add)
        {
            Result<Categorie> result = _categorieRepository.Handle(new AddCategorie(add.Nom));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        /// <summary>
        /// Modifie le libellé d'une catégorie existante.
        /// </summary>
        /// <param name="modifier">Les nouvelles informations de la catégorie.</param>
        /// <returns>Un résultat vide confirmant la modification ou un message d'erreur.</returns>
        [HttpPut]
        [Authorize(Roles = nameof(Role.Admin))]
        public IActionResult ModifierCategorie(ModifierCategorieDto modifier)
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
