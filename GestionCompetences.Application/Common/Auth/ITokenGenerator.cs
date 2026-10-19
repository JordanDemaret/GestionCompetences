using Ent = GestionCompetences.Entitie;

namespace GestionCompetences.Application.Common.Auth
{
    public interface ITokenGenerator
    {
        string Generator(Ent.Utilisateur utilisateur);

        string GenerateRefreshToken();

        Guid GetUserTokenId(string token);
    }
}
