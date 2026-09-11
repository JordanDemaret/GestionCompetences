using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Query;
using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Application.Competence.Repository
{
    public interface ICategorieRepository:
        IQueryHandler<GetCategorieListe,IEnumerable<Categorie>>,
        ICommandeHandler<AddCategorie>,
        ICommandeHandler<ModifierCategorie>
    {
    }
}
