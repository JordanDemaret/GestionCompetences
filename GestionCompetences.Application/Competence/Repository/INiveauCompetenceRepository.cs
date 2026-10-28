using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Entitie.Competence;


namespace GestionCompetences.Application.Competence.Repository
{
    /// <summary>
    /// Définit le contrat du référentiel pour la consultation des niveaux de compétences.
    /// </summary>
    public interface INiveauCompetenceRepository: 
        IQueryHandler<GetNiveauCompetenceListe, IEnumerable<NiveauCompetence>>
    {
    }
}
