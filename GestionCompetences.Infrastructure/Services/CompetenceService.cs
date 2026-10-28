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
    /// <summary>
    /// Service d'infrastructure implémentant le référentiel des compétences utilisateurs.
    /// </summary>
    public class CompetenceService : ICompetenceRepository
    {
        private readonly CompetenceDBContext _dBContext;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="CompetenceService"/>.
        /// </summary>
        /// <param name="dBContext">Le contexte de base de données.</param>
        public CompetenceService(CompetenceDBContext dBContext)
        {
            _dBContext = dBContext;
        } 
        
        
        /// <summary>
        /// Récupère la liste de toutes les compétences d'un utilisateur avec leurs relations (catégorie, niveau, certificat), triées par position.
        /// </summary>
        /// <param name="query">La requête contenant l'identifiant de l'utilisateur.</param>
        /// <returns>La liste des compétences sous forme de DTO ou une erreur.</returns>
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

        /// <summary>
        /// Ajoute une nouvelle compétence pour un utilisateur avec une position incrémentée automatiquement à la fin de sa liste.
        /// </summary>
        /// <param name="command">La commande contenant les informations de la compétence à insérer.</param>
        /// <returns>La compétence créée enrichie de ses dépendances sous forme de DTO ou une erreur.</returns>
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

        /// <summary>
        /// Modifie les propriétés d'une compétence appartenant à un utilisateur spécifique.
        /// </summary>
        /// <param name="command">La commande contenant les nouvelles valeurs de la compétence.</param>
        /// <returns>Un résultat en succès ou une erreur si la compétence n'existe pas.</returns>
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

        /// <summary>
        /// Supprime une compétence appartenant à un utilisateur et décrémente automatiquement les positions supérieures.
        /// </summary>
        /// <param name="command">La commande contenant l'identifiant de la compétence et de l'utilisateur.</param>
        /// <returns>Un résultat en succès ou une erreur si la compétence n'est pas trouvée.</returns>
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

        /// <summary>
        /// Réorganise et met à jour séquentiellement les positions d'une liste de compétences pour un utilisateur.
        /// </summary>
        /// <param name="command">La commande contenant la liste ordonnée des identifiants de compétences.</param>
        /// <returns>Un résultat en succès ou une erreur si l'une des compétences est invalide.</returns>
        public Result Handle(MiseAJourPossition command)
        {
            try
            {
                List<CompetenceUtilisateur> competences = _dBContext.Competences.Where(c => c.UtilisateurId == command.IdUtilisateur).ToList();
                int possition = 1;
                foreach(Guid item in command.IdCompetences)
                {
                    CompetenceUtilisateur? com = competences.FirstOrDefault(c => c.Id == item);
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
