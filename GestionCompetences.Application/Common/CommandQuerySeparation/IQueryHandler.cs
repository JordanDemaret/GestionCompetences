using GestionCompetences.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Common.CommandQuerySeparation
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQueryDefinition<TResult>
    {
        Result<TResult> Handle(TQuery query);
    }
}
