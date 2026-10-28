using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Application.Dto
{
    public record CertificatDto(Guid Id, string NomCertificat, string CodeCertificat, 
                                string? Editeur, DateTime DateObtention, Guid UtilisateurId, 
                                int NombreDeCompetencesLiees)
    {
        /// <summary>
        /// Mappe une entité <see cref="CertificatObtenu"/> vers un <see cref="CertificatDto"/>.
        /// </summary>
        /// <param name="certificat">L'entité certificat source.</param>
        /// <returns>Une nouvelle instance de <see cref="CertificatDto"/>.</returns>
        public static CertificatDto From(CertificatObtenu certificat)
            => new(certificat.Id, certificat.NomCertificat, certificat.CodeCertificat,
                    certificat.Editeur, certificat.DateObtention, certificat.UtilisateurId,
                    certificat.CompetencesList.Count);

    }
}
