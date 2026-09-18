using GestionCompetences.Application.Common.Auth;
using GestionCompetences.Domain.Enum;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


namespace GestionCompetences.Infrastructure.Auth
{
        public sealed class TokenGenerator(IOptions<JwtOptions> options, TimeProvider timeProvider) : ITokenGenerator
        {
            public AccessToken Generator(Guid utilisateurId, Role role)
            {
                JwtOptions jwt = options.Value;

                DateTimeOffset issuedAt = timeProvider.GetUtcNow();
                DateTimeOffset expiresAt = issuedAt.AddMinutes(jwt.ExpiryMinutes);

                var descriptor = new SecurityTokenDescriptor
                {
                    Issuer = jwt.Issuer,
                    Audience = jwt.Audience,
                    Subject = new ClaimsIdentity([new Claim(JwtRegisteredClaimNames.Sub, utilisateurId.ToString()),
                                                   new Claim(ClaimTypes.Role, role.ToString())]),
                    IssuedAt = issuedAt.UtcDateTime,
                    NotBefore = issuedAt.UtcDateTime,
                    Expires = expiresAt.UtcDateTime,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                        SecurityAlgorithms.HmacSha256)
                };

                return new AccessToken(new JsonWebTokenHandler().CreateToken(descriptor), expiresAt);
            }
    }
}
