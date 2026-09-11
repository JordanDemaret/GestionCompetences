using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GestionCompetences.API.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        public static Guid GetUtilisateurId(this ClaimsPrincipal principal)
        {

            string? subject = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(subject, out Guid utilisateurId)
                ? utilisateurId
                : throw new InvalidOperationException("Le jeton ne porte pas de claim 'sub' exploitable.");
        }
    }
}
