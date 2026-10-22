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
    public class CertificatService(CompetenceDBContext dBContext) : ICertificatRepository
    {
        private readonly CompetenceDBContext _dBContext = dBContext;

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
