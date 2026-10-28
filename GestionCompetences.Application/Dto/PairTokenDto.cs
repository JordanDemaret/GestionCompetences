
namespace GestionCompetences.Application.Dto
{
    public sealed record PairTokenDto(string Token, string RefreshToken)
    {
        /// <summary>
        /// Crée un nouveau <see cref="PairTokenDto"/> à partir d'un jeton d'accès et d'un jeton de rafraîchissement.
        /// </summary>
        /// <param name="Token">Le jeton d'accès JWT.</param>
        /// <param name="RefreshToken">Le jeton de rafraîchissement.</param>
        /// <returns>Une nouvelle instance de <see cref="PairTokenDto"/>.</returns>
        public static PairTokenDto From(string Token, string RefreshToken)
            =>new PairTokenDto(Token,RefreshToken);
    }
}
