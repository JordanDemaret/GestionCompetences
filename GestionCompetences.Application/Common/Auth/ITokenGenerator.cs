using GestionCompetences.Domain.Enum;

namespace GestionCompetences.Application.Common.Auth
{
    public interface ITokenGenerator
    {
        AccessToken Generator(Guid utilisateurId, Role role);
    }
}
