using GestionCompetences.Entitie;

namespace GestionCompetences.API.Dto
{
    public record UtilisateurDto(Guid Id, string Nom, string prenom, string Email, string Role, string Token, string RefreshToken)
    {

        public static UtilisateurDto From(Utilisateur utilisateur, string Token)
                => new UtilisateurDto(utilisateur.Id, utilisateur.Nom, utilisateur.Prenom, utilisateur.Email, utilisateur.Role.ToString(), Token, utilisateur.RefreshToken);

    }
}
