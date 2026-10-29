using GestionCompetences.Application.Dto.AttributDeValidation;
using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.Application.Dto
{
    public class ModifierCertificatDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string NomCertificat { get; set; } = string.Empty;

        [Required]
        [StringLength(250, MinimumLength = 1)]
        public string CodeCertificat { get; set; } = string.Empty;

        [StringLength(200)]
        public string Editeur { get; set; } = string.Empty;

        [Required]
        [DateAnnee(2000, ErrorMessage = "L'année doit être supérieure à 2000.")]
        public DateTime DateObtention { get; set; }
    }
}
