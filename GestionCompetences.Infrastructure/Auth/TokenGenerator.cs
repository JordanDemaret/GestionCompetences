using GestionCompetences.Application.Common.Auth;
using GestionCompetences.Entitie;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public Guid GetUserTokenId(string token)
        {
            JwtSecurityToken jwt = new JwtSecurityToken(token);
            Claim? sid = jwt.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid);

            if (sid is null)
                throw new InvalidOperationException("No Sid found");

            return Guid.Parse(sid.Value);
        }
    }
}
