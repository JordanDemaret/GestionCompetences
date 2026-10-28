using GestionCompetences.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Common.CommandQuerySeparation
{
    /// <summary>
    /// Gestionnaire de requête (Query) permettant de récupérer des données sans modifier l'état du système.
    /// </summary>
    /// <typeparam name="TQuery">Le type de requête à exécuter.</typeparam>
    /// <typeparam name="TResult">Le type de données retournées en résultat.</typeparam>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQueryDefinition<TResult>
    {
        /// <summary>
        /// Exécute la requête spécifiée et renvoie les données associées.
        /// </summary>
        /// <param name="query">La requête contenant les filtres ou critères de sélection.</param>
        /// <returns>Le résultat contenant les données demandées ou une erreur.</returns>
        Result<TResult> Handle(TQuery query);
    }
}
