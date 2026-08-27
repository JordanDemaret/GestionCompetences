using EFGestionCompetences.Enum;

namespace EFGestionCompetences.Entitie
{
    public class CompetenceUtilisateur
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public Statut Statu { get; set; }
        public bool EstVisibilite { get; set; }
        public string Note { get; set; } = string.Empty;
        public string Lien { get; set; } = string.Empty;
        public DateTime DateDeDebut { get; set; }

        public Guid? CatégorieId { get; set; } 
        public Categorie? Categorie { get; set; }

        public Guid UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; } = default!;

        public Guid? CertificatId { get; set; }
        public Certificat? certificat { get; set; }

    }
}
