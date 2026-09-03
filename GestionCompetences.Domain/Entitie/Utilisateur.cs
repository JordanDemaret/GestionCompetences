using GestionCompetences.Domain.Enum;
using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Entitie
{
    public class Utilisateur
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string MotDePasse { get; set; } = string.Empty;
        public bool EstActif { get; set; }
        public Role Role { get; set; }

        public ICollection<CompetenceUtilisateur> Competences = new List<CompetenceUtilisateur>();
        public IList<CertificatObtenu> CertificatsObtenus = new List<CertificatObtenu>();
    }
}
