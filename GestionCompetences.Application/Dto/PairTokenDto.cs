
namespace GestionCompetences.Application.Dto
{
    public sealed record PairTokenDto(string Token, string RefreshToken)
    {
        public static PairTokenDto From(string Token, string RefreshToken)
            =>new PairTokenDto(Token,RefreshToken);
    }
}
