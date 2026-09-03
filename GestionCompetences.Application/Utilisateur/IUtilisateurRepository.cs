using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Utilisateur.Commande;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Utilisateur
{
    public interface IUtilisateurRepository :
        ICommandeHandler<IncriptionCommande>
    {
    }
}
