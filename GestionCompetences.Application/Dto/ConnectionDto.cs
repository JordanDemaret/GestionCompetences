using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.API.Dto
{
    public class ConnectionDto
    {
        [Required]
        [StringLength(150, MinimumLength = 1)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 8)]
        public string MotDePasse { get; set; }
    }
}
