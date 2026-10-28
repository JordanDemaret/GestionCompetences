using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Query;
using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Application.Competence.Repository
{
    /// <summary>
    /// Définit le contrat du référentiel pour la gestion des catégories de compétences (consultation, ajout, modification).
    /// </summary>
    public interface ICategorieRepository:
        IQueryHandler<GetCategorieListe,IEnumerable<Categorie>>,
        ICommandeHandler<AddCategorie,Categorie>,
        ICommandeHandler<ModifierCategorie>
    {
    }
}
