using GestionCompetences.Application.Common.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Utilisateur.Commande
{
    public record ConnexionCommande(string Email, string MotDePasse) : ICommandDefinition;
}
