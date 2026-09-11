using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Entitie.Competence;


namespace GestionCompetences.Application.Competence.Repository
{
    public interface INiveauCompetenceRepository: 
        IQueryHandler<GetNiveauCompetenceListe, IEnumerable<NiveauCompetence>>,
        ICommandeHandler<AddNiveauCompetence>,
        ICommandeHandler<ModifierNiveauCompetence>
    {
    }
}
