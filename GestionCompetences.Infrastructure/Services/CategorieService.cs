using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Errors;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Query;
using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;

namespace GestionCompetences.Infrastructure.Services
{
    public class CategorieService : ICategorieRepository
    {
        private readonly CompetenceDBContext _dbContext;

        public CategorieService(CompetenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result<IEnumerable<Categorie>> Handle(GetCategorieListe query)
        {
            return Result<IEnumerable<Categorie>>.Success(_dbContext.Categories.AsEnumerable());
        }

        public Result Handle(AddCategorie command)
        {
            try
            {
                if (_dbContext.Categories.Any(c => c.Nom == command.Nom))
                {
                    return Result.Failure(CategorieErrors.CategorieNomExiste);
                }

                Categorie categorie = new Categorie()
                                        {
                                            Nom = command.Nom
                                        };
                _dbContext.Categories.Add(categorie);
                _dbContext.SaveChanges();
                return Result.Success(); 
            }
            catch(Exception)
            {
               return Result.Failure(CategorieErrors.CategorieException);
            }
        }

        public Result Handle(ModifierCategorie command)
        {
            try
            {
                Categorie? categorie = _dbContext.Categories.AsNoTracking()
                                    .SingleOrDefault(c => c.Id == command.Id);

                if (categorie is null)
                    return Result.Failure(CategorieErrors.CategoriePasTrouver);

                if (categorie.Nom == command.Nom)
                    return Result.Failure(CategorieErrors.CategoriePasModifier);

                if (_dbContext.Categories.Any(c => c.Nom == command.Nom))
                {
                   return Result.Failure(CategorieErrors.CategorieNomExiste);
                }

                return Result.Success();
            }
            catch (Exception)
            {
                return Result.Failure(CategorieErrors.CategorieException);
            }
            
        }
    }
}
