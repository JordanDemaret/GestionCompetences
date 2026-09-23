using GestionCompetences.Application.Common.Auth;
using GestionCompetences.Entitie;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace GestionCompetences.Infrastructure.Auth
{
    public sealed class TokenGenerator(IOptions<JwtOptions> options, TimeProvider timeProvider) : ITokenGenerator
    {
        public string Generator(Utilisateur utilisateur)
        {
            JwtOptions jwt = options.Value;
            
            byte[] secretKey = Encoding.Default.GetBytes(jwt.Key);
            SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(secretKey);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, utilisateur.Id.ToString()),
                new Claim(ClaimTypes.Role, utilisateur.Role.ToString())
            };

            JwtSecurityToken jwtSecurity = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwt.ExpiryMinutes),
                signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurity);
        }
    }
}
