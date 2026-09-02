namespace GestionCompetences.Entitie.Competence
{
    public class NiveauCompetence
    {
        public Guid Id { get; set; }
        public string Label { get; set; }

        public IEnumerable<CompetenceUtilisateur> CompetenceListe { get; set; } = new List<CompetenceUtilisateur>();
        public IEnumerable<HistoriqueProgression> Historiques { get; set; } = new List<HistoriqueProgression>();
        

    
    }
}
