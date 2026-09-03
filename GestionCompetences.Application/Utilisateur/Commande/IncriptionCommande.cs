
using GestionCompetences.Application.Common.CommandQuerySeparation;

namespace GestionCompetences.Application.Utilisateur.Commande
{
    public record IncriptionCommande(string Nom, string Prenom, string Email, string MaoDePassHaser) : ICommandDefinition;
}
