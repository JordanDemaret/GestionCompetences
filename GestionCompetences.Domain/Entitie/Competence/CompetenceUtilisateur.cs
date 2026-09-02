using GestionCompetences.Domain.Enum;
using GestionCompetences.Enum;

namespace GestionCompetences.Entitie.Competence
{
    public class CompetenceUtilisateur
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public Visibilite visibilite { get; set; }
        public string? Note { get; set; } = string.Empty;
        public string? Lien { get; set; } = string.Empty;
        public DateTime DateDeDebut { get; set; }
        public Statut  Statut { get; set; }
        public int Position { get; set; }
        public string NomGroupe { get; set; } = string.Empty;

        public Guid? CatégorieId { get; set; } 
        public Categorie? Categorie { get; set; }

        public Guid UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; } = default!;

        public Guid? CertificatId { get; set; }
        public CertificatObtenue? Certificat { get; set; }

        public Guid? ParantId { get; set; }
        public CompetenceUtilisateur? CompetenceParant { get; set; }

        public Guid NiveauId { get; set; }
        public NiveauCompetence Niveau { get; set; } = default!;

        public ICollection<HistoriqueProgression> Historiques { get; set; } = new List<HistoriqueProgression>();
        public ICollection<CompetenceUtilisateur> SousCompetences { get; set; } = new List<CompetenceUtilisateur>();

    }
}
