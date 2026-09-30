using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.API.Dto
{
    public class ModifierCategorieDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Nom { get; set; }
    }
}
