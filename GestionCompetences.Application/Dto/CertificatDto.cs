using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Application.Dto
{
    public record CertificatDto(Guid Id, string NomCertificat, string CodeCertificat, 
                                string? Editeur, DateTime DateObtention, Guid UtilisateurId, 
                                int NombreDNombreCompetencesLiees)
    {
        public static CertificatDto From(CertificatObtenu certificat)
            => new(certificat.Id, certificat.NomCertificat, certificat.CodeCertificat,
                    certificat.Editeur, certificat.DateObtention, certificat.UtilisateurId,
                    certificat.CompetencesLsite.Count);

    }
}
