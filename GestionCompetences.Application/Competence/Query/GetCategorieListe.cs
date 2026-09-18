using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Application.Query
{
    public record GetCategorieListe() : IQueryDefinition<IEnumerable<Categorie>>;
}
