using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.Application.Dto
{
    public class AddCertificatDto
    {
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string NomCertificat { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string CodeCertificat { get; set; } = string.Empty;

        [StringLength(250)]
        public string Editeur { get; set; } = string.Empty;

        [Required]
        public DateTime DateObtention { get; set; }
    }
}
