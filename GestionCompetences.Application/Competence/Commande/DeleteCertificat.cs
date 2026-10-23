using GestionCompetences.Application.Common.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace GestionCompetences.Application.Competence.Commande
{
    public record DeleteCertificat(Guid Id, Guid UtilisateurId) : ICommandDefinition
    {
    }
}
