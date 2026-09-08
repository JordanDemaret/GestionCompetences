namespace GestionCompetences.Application.Common.Auth
{
    public interface ITokenGenerator
    {
        AccessToken Generator(Guid utilisateurId);
    }
}
