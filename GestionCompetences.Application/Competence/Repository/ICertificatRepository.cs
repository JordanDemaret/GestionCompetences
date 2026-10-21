using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Dto;

namespace GestionCompetences.Application.Competence.Repository
{
    public interface ICertificatRepository :
        IQueryHandler<GetCertificatListe, IEnumerable<CertificatDto>>
    {
    }
}
