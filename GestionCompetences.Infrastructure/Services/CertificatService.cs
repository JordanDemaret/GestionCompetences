using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Errors;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

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
                                            .Select(CertificatDto.From);
                return Result<IEnumerable<CertificatDto>>.Success(certificats);

            }
            catch (Exception)
            {
                return Result<IEnumerable<CertificatDto>>.Failure(CertificatErrors.CertificatException);
            }
        }
    }
}
