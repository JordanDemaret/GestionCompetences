namespace GestionCompetences.Entitie.Competence
{
    public class Categorie
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public ICollection<CompetenceUtilisateur> CompetencesListe = new List<CompetenceUtilisateur>();
    }
}
