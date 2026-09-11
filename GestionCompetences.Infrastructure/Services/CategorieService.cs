using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Query;
using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

        public Result Handle(AddCompetence command)
        {
            try
            {
                if (_dbContext.Categories.Any(c => c.Nom == command.Nom))
                {
                    return Result.Failure(Error.Create("Non", "Non"));
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
               return Result.Failure(Error.Create("Non", "Non"));
            }
        }
    }
}
