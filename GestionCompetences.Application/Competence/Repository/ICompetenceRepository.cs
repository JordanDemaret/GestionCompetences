using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;

namespace GestionCompetences.Application.Competence.Repository
{
    public interface ICompetenceRepository :
        IQueryHandler<GetCompetenceUtilisateur,IEnumerable<CompetenceDto>>,
        ICommandeHandler<AddCompetence,CompetenceDto>,
        ICommandeHandler<ModifierCompetence>,
        ICommandeHandler<DeleteCompetence>,
        ICommandeHandler<MisAJourPossition>
    { 
    }

}
