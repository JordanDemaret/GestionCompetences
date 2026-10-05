using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Domain.Enum;
using GestionCompetences.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Competence.Commande
{
    public record ModifierCompetence(Guid Id,string Nom, string? Note, DateTime DateDeDebut, Visibilite Visibilite, Statut Statut, Guid CategorieId, Guid NiveauId) : ICommandDefinition;
}
