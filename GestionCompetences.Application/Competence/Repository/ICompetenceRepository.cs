using GestionCompetences.API.Dto;
using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Entitie.Competence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Competence.Repository
{
    public interface ICompetenceRepository :
        IQueryHandler<GetCompetenceUtilisateur,IEnumerable<CompetenceReadDto>>,
        ICommandeHandler<AddCompetence>
    { 
    }

}
