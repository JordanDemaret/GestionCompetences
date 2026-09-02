namespace GestionCompetences.Entitie.Competence
{
    public class HistoriqueProgression
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public Guid CompetenceId { get; set; }
        public CompetenceUtilisateur Competence { get; set; } = default!;

        public Guid? AncienNiveauId { get; set; }
        public NiveauCompetence? AncienNiveau { get; set; }

        public Guid NouveauNiveauId { get; set; }
        public NiveauCompetence NouveauNiveau { get; set; } = default!;



    }
}
