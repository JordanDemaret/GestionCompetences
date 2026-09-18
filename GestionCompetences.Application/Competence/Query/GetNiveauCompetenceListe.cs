using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Application.Competence.Query
{
    public record GetNiveauCompetenceListe() : IQueryDefinition<IEnumerable<NiveauCompetence>>;
}
