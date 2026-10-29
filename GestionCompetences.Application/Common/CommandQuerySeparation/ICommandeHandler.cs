using GestionCompetences.Application.Common.Results;

namespace GestionCompetences.Application.Common.CommandQuerySeparation
{
    /// <summary>
    /// Gestionnaire de commande exécutant une action sans valeur de retour spécifique.
    /// </summary>
    /// <typeparam name="TCommand">Le type de commande à traiter.</typeparam>
    public interface ICommandeHandler<TCommand>
        where TCommand : ICommandDefinition
    {
        /// <summary>
        /// Traite la commande spécifiée.
        /// </summary>
        /// <param name="command">L'instance de la commande contenant les paramètres de l'action.</param>
        /// <returns>Le résultat de l'exécution de la commande.</returns>
        Result Handle(TCommand command);
    }

    /// <summary>
    /// Gestionnaire de commande exécutant une action retournant un résultat de type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TCommand">Le type de commande à traiter.</typeparam>
    /// <typeparam name="TResult">Le type de la valeur retournée en cas de succès.</typeparam>
    public interface ICommandeHandler<TCommand,TResult>
        where TCommand : ICommandDefinition
    {
        /// <summary>
        /// Traite la commande spécifiée et renvoie un résultat typé.
        /// </summary>
        /// <param name="command">L'instance de la commande contenant les paramètres de l'action.</param>
        /// <returns>Le résultat de l'exécution contenant les données générées ou une erreur.</returns>
        Result<TResult> Handle(TCommand command);
    }
}
