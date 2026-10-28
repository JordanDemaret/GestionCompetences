using Ent = GestionCompetences.Entitie;

namespace GestionCompetences.Application.Common.Auth
{
    /// <summary>
    /// Définit le contrat pour la génération et la lecture de jetons JWT et jetons de rafraîchissement.
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Génère un jeton d'accès JWT pour un utilisateur donné avec ses revendications (claims).
        /// </summary>
        /// <param name="utilisateur">L'utilisateur pour lequel générer le jeton.</param>
        /// <returns>La chaîne encodée représentant le jeton JWT.</returns>
        string Generator(Ent.Utilisateur utilisateur);

        /// <summary>
        /// Génère un nouveau jeton aléatoire sécurisé destiné au rafraîchissement (Refresh Token).
        /// </summary>
        /// <returns>Une chaîne encodée en base64 représentant le jeton de rafraîchissement.</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Extrait l'identifiant unique (GUID) de l'utilisateur contenu dans un jeton JWT sans vérifier sa date d'expiration.
        /// </summary>
        /// <param name="token">Le jeton JWT à analyser.</param>
        /// <returns>L'identifiant GUID de l'utilisateur.</returns>
        Guid GetUserTokenId(string token);
    }
}
