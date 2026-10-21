using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
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
            string? utilisasteurIdString = User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(utilisasteurIdString) || !Guid.TryParse(utilisasteurIdString, out var utilisateurId))
                return BadRequest("Identifiant utilisateur invalide.");

            var result = _certificatRepository.Handle(new GetCertificatListe(utilisateurId));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }
    }
}
