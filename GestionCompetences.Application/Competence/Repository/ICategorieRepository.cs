using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Competence.Commande;
using GestionCompetences.Application.Query;
using GestionCompetences.Entitie.Competence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Competence.Repository
{
    public interface ICategorieRepository:
        IQueryHandler<GetCategorieListe,IEnumerable<Categorie>>,
        ICommandeHandler<AddCompetence>
    {
    }
}
