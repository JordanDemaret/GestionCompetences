using GestionCompetences.Entitie;

namespace GestionCompetences.API.Dto
{
    public record UtilisateurDto(Guid Id, string Nom, string prenom, string Email, string Role, string Token, string RefreshToken)
    {

        /// <summary>
        /// Mappe une entité <see cref="Utilisateur"/> et un jeton JWT vers un <see cref="UtilisateurDto"/>.
        /// </summary>
        /// <param name="utilisateur">L'entité utilisateur source.</param>
        /// <param name="Token">Le jeton d'accès JWT généré.</param>
        /// <returns>Une nouvelle instance de <see cref="UtilisateurDto"/>.</returns>
        public static UtilisateurDto From(Utilisateur utilisateur, string Token)
                => new UtilisateurDto(utilisateur.Id, utilisateur.Nom, utilisateur.Prenom, utilisateur.Email, utilisateur.Role.ToString(), Token, utilisateur.RefreshToken);

    }
}
