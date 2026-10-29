using GestionCompetences.Application.Common.Auth;
using GestionCompetences.Entitie;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace GestionCompetences.Infrastructure.Auth
{
    /// <summary>
    /// Service responsable de la création de jetons JWT sécurisés et de jetons de rafraîchissement.
    /// </summary>
    public sealed class TokenGenerator(IOptions<JwtOptions> options, TokenValidationParameters tokenValidationParameters) : ITokenGenerator
    {

        /// <summary>
        /// Construit et signe un jeton JWT contenant l'identifiant (SID) et le rôle de l'utilisateur.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur pour lequel générer le jeton JWT.</param>
        /// <returns>La chaîne encodée du jeton JWT.</returns>
        public string Generator(Utilisateur utilisateur)
        {
            JwtOptions jwt = options.Value;
            
            byte[] secretKey = Encoding.UTF8.GetBytes(jwt.Key);
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
        /// <summary>
        /// Génère une chaîne aléatoire sécurisée de 32 octets encodée en base64 pour servir de Refresh Token.
        /// </summary>
        /// <returns>Le jeton de rafraîchissement sous forme de chaîne de caractères.</returns>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Valide le format d'un jeton JWT (sans vérifier son expiration) et extrait l'identifiant (SID) de l'utilisateur.
        /// </summary>
        /// <param name="token">Le jeton JWT à analyser.</param>
        /// <returns>Le GUID de l'utilisateur extrait du jeton.</returns>
        /// <exception cref="InvalidOperationException">Lancée lorsqu'aucune revendication de type SID n'est trouvée dans le jeton.</exception>
        public Guid GetUserTokenId(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var para = tokenValidationParameters.Clone();
            para.ValidateLifetime = false;

            handler.ValidateToken(token, para, out SecurityToken validatedToken);
            JwtSecurityToken? jwt = validatedToken as JwtSecurityToken;

            Claim? sid = jwt?.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid);

            if (sid is null)
                throw new InvalidOperationException("No Sid found");

            return Guid.Parse(sid.Value);
        }
    }
}
