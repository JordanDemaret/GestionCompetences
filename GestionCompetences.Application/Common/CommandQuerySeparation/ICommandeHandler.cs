using GestionCompetences.Application.Common.Results;

namespace GestionCompetences.Application.Common.CommandQuerySeparation
{
    public interface ICommandeHandler<TCommand>
        where TCommand : ICommandDefinition
    {
        Result Handle(TCommand command);
    }
}
