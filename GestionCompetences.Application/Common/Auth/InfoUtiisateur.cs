using Ent = GestionCompetences.Entitie;

namespace GestionCompetences.Application.Common.Auth
{
    public sealed record InfoUtiisateur(string Token, Ent.Utilisateur Utilisateur);
}
