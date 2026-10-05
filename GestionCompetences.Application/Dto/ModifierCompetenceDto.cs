using GestionCompetences.Domain.Enum;
using GestionCompetences.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionCompetences.Application.Dto
{
    public class ModifierCompetenceDto
    {

        [Required]
        public Guid Id { get; set; }

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
