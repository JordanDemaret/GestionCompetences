namespace GestionCompetences.API.Dto
{
    public class CompetenceReadDto
    {

        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime DateDeDebut { get; set; }
        public int Visibilite { get; set; }
        public int Statut { get; set; }
        public int Position { get; set; }

        public Guid CategorieId { get; set;} 
        public Guid  NiveauId { get; set; }



       
    }
}
