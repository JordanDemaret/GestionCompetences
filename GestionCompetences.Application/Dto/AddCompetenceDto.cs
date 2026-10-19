using GestionCompetences.Domain.Enum;
using GestionCompetences.Enum;
using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.API.Dto
{
    public class AddCompetenceDto
    {
        [Required]
        public string Nom { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        [Required]
        public DateTime DateDeDebut { get; set; }

        [Required]
        public Visibilite Visibilite { get; set; }

        [Required]
        public Statut Statut { get; set; }
        
        public int Position { get; set; }

        [Required]
        public Guid CategorieId { get; set; }

        [Required]
        public Guid NiveauId { get; set; }
    }
}
