using GestionCompetences.Application.Common.Results;

namespace GestionCompetences.Application.Competence.Errors
{
    public class CompetenceErrors
    {
        public static Error CompetenceException => Error.Create("Compétence.Exception", "Une erreur est survenue avec le serveur.");
        public static Error CompetenceNonTrouver => Error.Create("Compétence.Non.Trouver", "La compétence n'est pas trouvable");
    }
}
