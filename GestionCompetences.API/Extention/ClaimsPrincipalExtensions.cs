using System.Security.Claims;

namespace GestionCompetences.API.Extention
{
    /// <summary>
    /// Fournit des méthodes d'extension pour l'objet <see cref="ClaimsPrincipal"/>.
    /// </summary>
    internal static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Extrait l'identifiant unique (GUID) de l'utilisateur à partir du claim <see cref="ClaimTypes.Sid"/>.
        /// </summary>
        /// <param name="user">L'utilisateur connecté.</param>
        /// <returns>Le GUID de l'utilisateur s'il est valide, sinon <c>null</c>.</returns>
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var userIdString = user.FindFirstValue(ClaimTypes.Sid);

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return null;
            }

            return userId;
        }
    }
}
