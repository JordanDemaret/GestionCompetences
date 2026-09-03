namespace GestionCompetences.Application.Common.CommandQuerySeparation
{
    public interface ICommandeHandler<TCommand>
        where TCommand : ICommandDefinition
    {
        void Handle(TCommand command);
    }
}
