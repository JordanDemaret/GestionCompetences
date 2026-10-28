using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Dto;

namespace GestionCompetences.Application.Competence.Repository
{
    /// <summary>
    /// Définit le contrat du référentiel pour la gestion des certificats utilisateurs (consultation, ajout, modification, suppression).
    /// </summary>
    public interface ICertificatRepository :
        IQueryHandler<GetCertificatListe, IEnumerable<CertificatDto>>,
        ICommandeHandler<AddCertificat,CertificatDto>,
        ICommandeHandler<ModifierCertificat>,
        ICommandeHandler<DeleteCertificat>
    {
    }
}
