using GestionCompetences.Application.Common.CommandQuerySeparation;

namespace GestionCompetences.Application.Competence.Commande
{
    public record AddCategorie(string Nom) : ICommandDefinition;
}
