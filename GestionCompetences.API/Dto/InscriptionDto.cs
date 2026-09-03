using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionCompetences.Application.form
{
    public class InscriptionDto
    {
        [Required]
        [StringLength(150, MinimumLength =1)]
        public string Nom { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Prenom { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 1)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 8)]
        public string MotDePasse { get; set; }
    }
}
