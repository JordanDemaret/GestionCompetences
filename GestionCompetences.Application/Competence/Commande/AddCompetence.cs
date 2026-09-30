using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Domain.Enum;
using GestionCompetences.Enum;

namespace GestionCompetences.Application.Competence.Commande
{
    public record AddCompetence(string Nom, string? Note, DateTime DateDeDebut, Visibilite Visibilite, Statut Statut, int Position, Guid CategorieId, Guid UtilisateurId, Guid NiveauId) : ICommandDefinition;
}
