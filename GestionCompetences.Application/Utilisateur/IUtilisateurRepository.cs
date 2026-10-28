using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Dto;
using GestionCompetences.Application.Utilisateur.Commande;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Utilisateur
{
    /// <summary>
    /// Définit le contrat du référentiel pour la gestion des utilisateurs et de l'authentification (inscription, connexion, rafraîchissement de token).
    /// </summary>
    public interface IUtilisateurRepository :
        ICommandeHandler<IncriptionCommande,Guid>,
        ICommandeHandler<ConnexionCommande,UtilisateurDto>,
        ICommandeHandler<RefreshTokenCommande,PairTokenDto>
    {
    }
}
