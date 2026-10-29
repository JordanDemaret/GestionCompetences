using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Errors;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Dto;
using GestionCompetences.Entitie.Competence;
using Microsoft.EntityFrameworkCore;

namespace GestionCompetences.Infrastructure.Services
{
    /// <summary>
    /// Service d'infrastructure implémentant le référentiel des certificats obtenus par les utilisateurs.
    /// </summary>
    public class CertificatService(CompetenceDBContext dBContext) : ICertificatRepository
    {
        private readonly CompetenceDBContext _dBContext = dBContext;

        /// <summary>
        /// Récupère la liste de tous les certificats appartenant à un utilisateur avec le compte de leurs compétences associées.
        /// </summary>
        /// <param name="query">La requête contenant l'identifiant de l'utilisateur.</param>
        /// <returns>La liste des certificats sous forme de DTO ou une erreur.</returns>
        public Result<IEnumerable<CertificatDto>> Handle(GetCertificatListe query)
        {
            try
            {
                IEnumerable<CertificatDto> certificats = _dBContext.Certificats
                                            .Where(c => c.UtilisateurId == query.UtilisateurId)
                                            .Include(c => c.CompetencesList)
                                            .Select(CertificatDto.From).ToList();
                return Result<IEnumerable<CertificatDto>>.Success(certificats);

            }
            catch (Exception)
            {
                return Result<IEnumerable<CertificatDto>>.Failure(CertificatErrors.CertificatException);
            }
        }

        /// <summary>
        /// Ajoute et persiste un nouveau certificat pour l'utilisateur spécifié.
        /// </summary>
        /// <param name="command">La commande contenant les informations du certificat à enregistrer.</param>
        /// <returns>Le certificat créé sous forme de DTO ou une erreur.</returns>
        public Result<CertificatDto> Handle(AddCertificat command)
        {
            try
            {
                CertificatObtenu certificat = new CertificatObtenu()
                {
                    NomCertificat = command.NomCertificat,
                    CodeCertificat = command.CodeCertificat,
                    Editeur = command.Editeur,
                    DateObtention = command.DateObtention,
                    UtilisateurId = command.UtilisateurId
                };
                _dBContext.Certificats.Add(certificat);
                _dBContext.SaveChanges();
                return Result<CertificatDto>.Success(CertificatDto.From(certificat));
            }
            catch
            {
                return Result<CertificatDto>.Failure(CertificatErrors.CertificatException);

            }
        }

        /// <summary>
        /// Modifie les détails d'un certificat appartenant à un utilisateur spécifique.
        /// </summary>
        /// <param name="command">La commande contenant les informations mises à jour du certificat.</param>
        /// <returns>Un résultat en succès ou une erreur si le certificat est introuvable.</returns>
        public Result Handle(ModifierCertificat command)
        {
            try
            {
                CertificatObtenu? certificate = _dBContext.Certificats.SingleOrDefault(c =>  
                                                                       c.Id == command.Id &&  c.UtilisateurId == command.UtilisateurId);

                if (certificate is null)
                    return Result.Failure(CertificatErrors.CertificatNonTrouver);

                certificate.NomCertificat = command.NomCertificat;
                certificate.CodeCertificat = command.CodeCertificat;
                certificate.Editeur = command.Editeur;
                certificate.DateObtention = command.DateObtention;

                _dBContext.SaveChanges();
                return Result.Success();
            }
            catch
            {
                return Result.Failure(CertificatErrors.CertificatException);
            }
        }

        /// <summary>
        /// Supprime un certificat appartenant à un utilisateur spécifique.
        /// </summary>
        /// <param name="command">La commande contenant l'identifiant du certificat et de l'utilisateur.</param>
        /// <returns>Un résultat en succès ou une erreur si le certificat n'existe pas.</returns>
        public Result Handle(DeleteCertificat command)
        {
            try
            {
                CertificatObtenu? certificate = _dBContext.Certificats
                                                        .Include(c => c.CompetencesList)
                                                        .SingleOrDefault(c =>c.Id == command.Id && c.UtilisateurId == command.UtilisateurId);

                if (certificate is null)
                    return Result.Failure(CertificatErrors.CertificatNonTrouver);

                _dBContext.Certificats.Remove(certificate);
                _dBContext.SaveChanges();
                return Result.Success();

            }
            catch
            {
                return Result.Failure(CertificatErrors.CertificatException);
            }
        }
    }
}
