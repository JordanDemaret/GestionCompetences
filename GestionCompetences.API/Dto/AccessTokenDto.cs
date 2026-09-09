using GestionCompetences.Application.Common.Auth;

namespace GestionCompetences.API.Dto
{
    public record AccessTokenDto(string AccessToken, DateTimeOffset ExpiresAt)
    {
       public static AccessTokenDto From(AccessToken access)
       => new(access.Value, access.ExpiresAt);

    }
}
