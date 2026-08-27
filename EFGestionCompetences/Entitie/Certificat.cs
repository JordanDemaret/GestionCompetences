using System;
using System.Collections.Generic;
using System.Text;

namespace EFGestionCompetences.Entitie
{
    public class Certificat
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Editeur { get; set; } = string.Empty;

        public Guid UtilsateurId { get; set; }
        public Utilisateur utilisateur { get; set; } = default!;

        public IList<CompetenceUtilisateur> competenceUtilisateurs { get; set; } = new List<CompetenceUtilisateur>();
    }
}
