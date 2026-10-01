using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Infrastructure.Services
{
    public class CompetenceService : ICompetenceRepository
    {
        private readonly CompetenceDBContext _dBContext;

        public CompetenceService(CompetenceDBContext dBContext)
        {
            _dBContext = dBContext;
        } 
        
        
        public Result<IEnumerable<CompetenceDto>> Handle(GetCompetenceUtilisateur query)
        {
            try
            {
                IEnumerable<CompetenceDto> competenceUtilisateurs = _dBContext.Competences
                                                                        .Where(c => c.UtilisateurId == query.IdUtilisateur)
                                                                        .Select(CompetenceDto.From).AsEnumerable();
                return Result<IEnumerable<CompetenceDto>>.Success(competenceUtilisateurs);
            }
            catch
            {
                return Result<IEnumerable<CompetenceDto>>.Failure(Error.Create("error", "error"));
            }
        }

        public Result Handle(AddCompetence command)
        {
            try
            {
                CompetenceUtilisateur competence = new CompetenceUtilisateur()
                {
                    Nom = command.Nom,
                    Note = command.Note,
                    DateDeDebut = command.DateDeDebut,
                    Visibilite = command.Visibilite,
                    Statut = command.Statut,
                    Position = command.Position,
                    CategorieId = command.CategorieId,
                    UtilisateurId = command.UtilisateurId,
                    NiveauId = command.NiveauId
                };

                _dBContext.Competences.Add(competence);
                _dBContext.SaveChanges();
                return Result.Success();
            }
            catch(Exception)
            {
                return Result.Failure(Error.Create("error", "error"));
            }
            
        }

       
    }
}
