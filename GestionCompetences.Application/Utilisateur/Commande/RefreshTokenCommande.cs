using GestionCompetences.Application.Common.CommandQuerySeparation;

namespace GestionCompetences.Application.Utilisateur.Commande
{
    public record RefreshTokenCommande(string Token, string RefreshToken): ICommandDefinition;
}
