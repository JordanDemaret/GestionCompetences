using GestionCompetences.Application.Common.Auth;
using GestionCompetences.Domain.Enum;

namespace GestionCompetences.API.Dto
{
    public class UtilisateurDto(Guid id, string nom, string prenom, string email, string role, string token, string refreshToken)
    {
        public Guid Id { get; } = id;
        public string Nom { get; } = nom;
        public string Prenom { get; } = prenom;
        public string Email { get; } = email;
        public string Role { get; } = role;
        public string Token { get; internal set; } = token;
        public string RefreshToken { get; internal set; } = refreshToken;

    }
}
