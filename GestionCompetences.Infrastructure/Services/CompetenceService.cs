using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Errors;
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
                                                                        .Include(c => c.CertificatObtenu)
                                                                        .Where(c => c.UtilisateurId == query.IdUtilisateur)
                                                                        .Select(CompetenceDto.From)
                                                                        .OrderBy(p => p.Position).ToList();
                return Result<IEnumerable<CompetenceDto>>.Success(competenceUtilisateurs);
            }
            catch
            {
                return Result<IEnumerable<CompetenceDto>>.Failure(CompetenceErrors.CompetenceException);
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
                    CertificatId = command.CertificatId
                    
                };

                _dBContext.Competences.Add(competence);
                _dBContext.SaveChanges();

                CompetenceUtilisateur createdCompetence = _dBContext.Competences
                    .Include(c => c.Categorie)
                    .Include(c => c.Niveau)
                    .Include(c => c.CertificatObtenu)
                    .First(c => c.Id == competence.Id);

                return Result<CompetenceDto>.Success(CompetenceDto.From(createdCompetence));
            }
            catch(Exception)
            {
                return Result<CompetenceDto>.Failure(CompetenceErrors.CompetenceException);
            }
            
        }

        public Result Handle(ModifierCompetence command)
        {
            try
            {
                CompetenceUtilisateur? competence = _dBContext.Competences.SingleOrDefault(c => c.Id == command.Id && c.UtilisateurId == command.UtilisateurId);
                if(competence is null)
                    return Result.Failure(CompetenceErrors.CompetenceNonTrouver);


                competence.Nom = command.Nom;
                competence.Note = command.Note;
                competence.DateDeDebut = command.DateDeDebut;
                competence.Visibilite = command.Visibilite;
                competence.Statut = command.Statut;
                competence.CategorieId = command.CategorieId;
                competence.NiveauId = command.NiveauId;
                competence.CertificatId = command.CertificatId;

                _dBContext.SaveChanges();
                return Result.Success();
            }
            catch
            {
                return Result.Failure(CompetenceErrors.CompetenceException);
            }

        }

        public Result Handle(DeleteCompetence command)
        {
            try
            {
                CompetenceUtilisateur? competence = _dBContext.Competences.SingleOrDefault(c => c.Id == command.id && c.UtilisateurId == command.UserId);

                if(competence is null)
                    return Result.Failure(CompetenceErrors.CompetenceNonTrouver);

                _dBContext.Competences.Remove(competence);
                int positionSupp = competence.Position;

                List<CompetenceUtilisateur> competences = _dBContext.Competences
                                                                    .Where(c => c.UtilisateurId == command.UserId 
                                                                                && c.Position > positionSupp)
                                                                    .ToList();

                foreach (CompetenceUtilisateur comp in competences)
                {
                    comp.Position--;
                } 

                _dBContext.SaveChanges();
                return Result.Success();
            }
            catch
            {
                return Result.Failure(CompetenceErrors.CompetenceException);
            }
        }

        public Result Handle(MisAJourPossition command)
        {
            try
            {
                List<CompetenceUtilisateur> competences = _dBContext.Competences.Where(c => c.UtilisateurId == command.IdUtilisateur).ToList();
                int possition = 1;
                foreach(string item in command.IdCompetences)
                {
                    Guid.TryParse(item, out Guid parsedId);
                    CompetenceUtilisateur? com = competences.FirstOrDefault(c => c.Id == parsedId);
                    if (com != null)
                    {
                        com.Position = possition++;
                    }
                    else
                    {
                        return Result.Failure(CompetenceErrors.CompetenceException);
                    }
                }

                _dBContext.SaveChanges();
                return Result.Success();

            }
            catch
            {
                return Result.Failure(CompetenceErrors.CompetenceException);
            }
        }
    }
}
