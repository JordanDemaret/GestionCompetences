using System;
using System.Collections.Generic;
using System.Text;

namespace EFGestionCompetences.Entitie
{
    public class Utilisateur
    {
        public Guid ID { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public bool esActif { get; set; }

        public IList<CompetenceUtilisateur> CompetenceUtilisateurs = new List<CompetenceUtilisateur>();
        public IList<Certificat> Certificats = new List<Certificat>();
    }
}
