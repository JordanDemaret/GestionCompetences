using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Errors;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Query;
using GestionCompetences.Entitie.Competence;
namespace GestionCompetences.Infrastructure.Services
{
    /// <summary>
    /// Service d'infrastructure implémentant le référentiel des catégories de compétences.
    /// </summary>
    public class CategorieService : ICategorieRepository
    {
        private readonly CompetenceDBContext _dbContext;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="CategorieService"/>.
        /// </summary>
        /// <param name="dbContext">Le contexte de base de données de l'application.</param>
        public CategorieService(CompetenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Récupère la liste complète des catégories de compétences enregistrées.
        /// </summary>
        /// <param name="query">La requête d'obtention de la liste des catégories.</param>
        /// <returns>La liste des catégories ou une erreur en cas d'exception.</returns>
        public Result<IEnumerable<Categorie>> Handle(GetCategorieListe query)
        {
            try
            {
                return Result<IEnumerable<Categorie>>.Success(_dbContext.Categories.AsEnumerable());
            }
            catch
            {
                return Result<IEnumerable<Categorie>>.Failure(CategorieErrors.CategorieException);
            }
        }

        /// <summary>
        /// Ajoute une nouvelle catégorie de compétences si un nom identique n'existe pas déjà.
        /// </summary>
        /// <param name="command">La commande contenant le nom de la catégorie à créer.</param>
        /// <returns>La catégorie créée avec son identifiant ou une erreur métier.</returns>
        public Result<Categorie> Handle(AddCategorie command)
        {
            try
            {
                if (_dbContext.Categories.Any(c => c.Nom == command.Nom))
                {
                    return Result<Categorie>.Failure(CategorieErrors.CategorieNomExiste);
                }

                Categorie categorie = new Categorie()
                                        {
                                            Nom = command.Nom
                                        };
                _dbContext.Categories.Add(categorie);
                _dbContext.SaveChanges();
                return Result<Categorie>.Success(categorie); 
            }
            catch(Exception)
            {
               return Result<Categorie>.Failure(CategorieErrors.CategorieException);
            }
        }

        /// <summary>
        /// Modifie le nom d'une catégorie existante après vérification de son existence et de l'unicité du nouveau nom.
        /// </summary>
        /// <param name="command">La commande contenant l'identifiant et le nouveau nom de la catégorie.</param>
        /// <returns>Un résultat en succès ou une erreur métier appropriée.</returns>
        public Result Handle(ModifierCategorie command)
        {
            try
            {
                Categorie? categorie = _dbContext.Categories.SingleOrDefault(c => c.Id == command.Id);

                if (categorie is null)
                    return Result.Failure(CategorieErrors.CategoriePasTrouver);

                if (categorie.Nom == command.Nom)
                    return Result.Failure(CategorieErrors.CategoriePasModifier);

                if (_dbContext.Categories.Any(c => c.Nom == command.Nom))
                {
                   return Result.Failure(CategorieErrors.CategorieNomExiste);
                }

                categorie.Nom = command.Nom;
                _dbContext.SaveChanges();

                return Result.Success();
            }
            catch (Exception)
            {
                return Result.Failure(CategorieErrors.CategorieException);
            }
            
        }
    }
}
