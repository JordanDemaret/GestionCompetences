using GestionCompetences.Application.Common.CommandQuerySeparation;

namespace GestionCompetences.Application.Competence.Commande
{
    public record MisAJourPossition(string[] IdCompetences, Guid IdUtilisateur) : ICommandDefinition
    {
    }
}
