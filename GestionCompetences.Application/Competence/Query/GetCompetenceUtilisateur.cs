using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Entitie.Competence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Competence.Query
{
    public record GetCompetenceUtilisateur(Guid IdUtilisateur) : IQueryDefinition<IEnumerable<CompetenceReadDto>>
    {
    }
}
