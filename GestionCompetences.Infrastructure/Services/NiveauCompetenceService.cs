using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Errors;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Infrastructure.Services
{
    /// <summary>
    /// Service d'infrastructure implémentant le référentiel des niveaux de compétences.
    /// </summary>
    public class NiveauCompetenceService : INiveauCompetenceRepository
    {
        private readonly CompetenceDBContext _dBContext;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="NiveauCompetenceService"/>.
        /// </summary>
        /// <param name="dBContext">Le contexte de base de données.</param>
        public NiveauCompetenceService(CompetenceDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        /// <summary>
        /// Récupère l'ensemble des niveaux de compétences prédéfinis dans la base de données.
        /// </summary>
        /// <param name="query">La requête d'obtention de la liste des niveaux.</param>
        /// <returns>La liste des niveaux de compétences ou une erreur.</returns>
        public Result<IEnumerable<NiveauCompetence>> Handle(GetNiveauCompetenceListe query)
        {
            try
            {
                return Result<IEnumerable<NiveauCompetence>>.Success(_dBContext.Niveau.AsEnumerable());
            }
            catch(Exception)
            {
                return Result<IEnumerable<NiveauCompetence>>.Failure(NiveauCompetenceErrors.NiveauCompetencesException);
            }
        }
    }
}
