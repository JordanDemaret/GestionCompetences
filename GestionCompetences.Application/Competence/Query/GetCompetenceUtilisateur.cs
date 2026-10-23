using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.CommandQuerySeparation;


namespace GestionCompetences.Application.Competence.Query
{
    public record GetCompetenceUtilisateur(Guid IdUtilisateur) : IQueryDefinition<IEnumerable<CompetenceDto>>
    {
    }
}
