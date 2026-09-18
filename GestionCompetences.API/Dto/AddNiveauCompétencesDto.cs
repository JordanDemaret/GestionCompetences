using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.API.Dto
{
    public class addNiveauCompétencesDto
    {
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Nom { get; set; }
    }
}
