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
    /// <summary>
    /// Contrôleur gérant les compétences des utilisateurs.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/competence")]
    public class CompetenceController(ICompetenceRepository competenceRepository) : ControllerBase
    {

        private readonly ICompetenceRepository _competenceRepository = competenceRepository;

        /// <summary>
        /// Récupère l'ensemble des compétences de l'utilisateur connecté, triées par position.
        /// </summary>
        /// <returns>La liste des compétences de l'utilisateur.</returns>
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

        /// <summary>
        /// Ajoute une nouvelle compétence pour l'utilisateur connecté avec une position calculée automatiquement.
        /// </summary>
        /// <param name="dto">Les informations de la compétence à ajouter.</param>
        /// <returns>La compétence créée contenant les informations enrichies ou une erreur.</returns>
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

        /// <summary>
        /// Modifie une compétence existante appartenant à l'utilisateur connecté.
        /// </summary>
        /// <param name="dto">Les nouvelles informations de la compétence.</param>
        /// <returns>Un résultat vide confirmant la modification ou une erreur si la compétence n'existe pas.</returns>
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

        /// <summary>
        /// Supprime une compétence de l'utilisateur connecté et décale les positions des compétences restantes.
        /// </summary>
        /// <param name="id">L'identifiant unique de la compétence à supprimer.</param>
        /// <returns>Un résultat vide confirmant la suppression ou une erreur.</returns>
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

        /// <summary>
        /// Met à jour l'ordre d'affichage des compétences d'un utilisateur selon une liste ordonnée d'identifiants.
        /// </summary>
        /// <param name="dto">Les identifiants ordonnés des compétences à repositionner.</param>
        /// <returns>Un résultat vide confirmant la réorganisation ou une erreur.</returns>
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
