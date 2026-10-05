using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;

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
                                                                        .Include(c => c.Categorie)
                                                                        .Include(c => c.Niveau)
                                                                        .Where(c => c.UtilisateurId == query.IdUtilisateur)
                                                                        .Select(CompetenceDto.From).AsEnumerable();
                return Result<IEnumerable<CompetenceDto>>.Success(competenceUtilisateurs);
            }
            catch
            {
                return Result<IEnumerable<CompetenceDto>>.Failure(Error.Create("error", "error"));
            }
        }

        public Result<CompetenceDto> Handle(AddCompetence command)
        {
            try
            {
                int nexPossition = _dBContext.Competences.Where(c => c.UtilisateurId == command.UtilisateurId).Count() + 1;


                CompetenceUtilisateur competence = new CompetenceUtilisateur()
                {
                    Nom = command.Nom,
                    Note = command.Note,
                    DateDeDebut = command.DateDeDebut,
                    Visibilite = command.Visibilite,
                    Statut = command.Statut,
                    Position = nexPossition,
                    CategorieId = command.CategorieId,
                    UtilisateurId = command.UtilisateurId,
                    NiveauId = command.NiveauId,
                    
                };

                _dBContext.Competences.Add(competence);
                _dBContext.SaveChanges();

                CompetenceUtilisateur createdCompetence = _dBContext.Competences
                    .Include(c => c.Categorie)
                    .Include(c => c.Niveau)
                    .First(c => c.Id == competence.Id);

                return Result<CompetenceDto>.Success(CompetenceDto.From(createdCompetence));
            }
            catch(Exception)
            {
                return Result<CompetenceDto>.Failure(Error.Create("error", "error"));
            }
            
        }

        public Result Handle(ModifierCompetence command)
        {
            try
            {
                CompetenceUtilisateur? competence = _dBContext.Competences.SingleOrDefault(c => c.Id == command.Id);
                if(competence is null)
                    return Result.Failure(Error.Create("error", "error"));


                competence.Nom = command.Nom;
                competence.Note = command.Note;
                competence.DateDeDebut = competence.DateDeDebut;
                competence.Visibilite = competence.Visibilite;
                competence.Statut = competence.Statut;
                competence.CategorieId = competence.CategorieId;
                competence.NiveauId = competence.NiveauId;

                _dBContext.SaveChanges();
                return Result.Success();
            }
            catch
            {
                return Result.Failure(Error.Create("error", "error"));
            }

        }

    }
}
