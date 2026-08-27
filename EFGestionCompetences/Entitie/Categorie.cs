using System;
using System.Collections.Generic;
using System.Text;

namespace EFGestionCompetences.Entitie
{
    public class Categorie
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public IList<CompetenceUtilisateur> CompetenceUtilisateurs = new List<CompetenceUtilisateur>();

   
    }
}
