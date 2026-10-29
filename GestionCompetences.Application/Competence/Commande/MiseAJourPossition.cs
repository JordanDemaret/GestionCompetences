using GestionCompetences.Application.Common.CommandQuerySeparation;

namespace GestionCompetences.Application.Competence.Commande
{
    public record MiseAJourPossition(Guid[] IdCompetences, Guid IdUtilisateur) : ICommandDefinition
    {
    }
}
