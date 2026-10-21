using GestionCompetences.Application.Common.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Competence.Commande
{
    public record AddCertificat(string NomCertificat, string CodeCertificat,string? Editeur,
                    DateTime DateObtention, Guid UtilisateurId): ICommandDefinition
    {
    }
}
