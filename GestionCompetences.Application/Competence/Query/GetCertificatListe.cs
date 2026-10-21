using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Dto;

namespace GestionCompetences.Application.Competence.Query
{
    public record GetCertificatListe(Guid UtilisateurId) : IQueryDefinition<IEnumerable<CertificatDto>>
    {
    }
}
