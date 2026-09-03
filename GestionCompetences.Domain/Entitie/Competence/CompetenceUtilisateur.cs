using GestionCompetences.Domain.Enum;
using GestionCompetences.Enum;

namespace GestionCompetences.Entitie.Competence
{
    public class CompetenceUtilisateur
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public Visibilite Visibilite { get; set; }
        public string? Note { get; set; }
        public string? Lien { get; set; }
        public DateTime DateDeDebut { get; set; }
        public Statut Statut { get; set; }
        public int Position { get; set; }
        public string NomGroupe { get; set; } = string.Empty;



        public Guid CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        public Guid UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; } = default!;

        public Guid? CertificatId { get; set; }
        public CertificatObtenu? CertificatObtenu { get; set; }


        public Guid NiveauId { get; set; }
        public NiveauCompetence Niveau { get; set; } = default!;

        public ICollection<HistoriqueProgression> Historiques { get; set; } = new List<HistoriqueProgression>();

    }
}
