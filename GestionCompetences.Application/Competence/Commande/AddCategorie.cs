using GestionCompetences.Application.Common.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Competence.Commande
{
    public record AddCategorie(string Nom) : ICommandDefinition;
}
