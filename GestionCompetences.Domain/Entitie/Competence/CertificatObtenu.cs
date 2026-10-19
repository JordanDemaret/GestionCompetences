namespace GestionCompetences.Entitie.Competence
{
    public class CertificatObtenu
    {
        public Guid Id { get; set; }
        public string NomCertificat { get; set; } = string.Empty;
        public string CodeCertificat { get; set; } = string.Empty;
        public string? Editeur { get; set; } = string.Empty;
        public DateTime DateObtention { get; set; }

        public Guid UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; } = default!;

        public ICollection<CompetenceUtilisateur> CompetencesLsite { get; set; } = new List<CompetenceUtilisateur>();

    }
}
