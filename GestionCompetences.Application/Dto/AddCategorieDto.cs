using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.API.Dto
{
    public class AddCategorieDto
    {
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Nom { get; set; }
    }
}
