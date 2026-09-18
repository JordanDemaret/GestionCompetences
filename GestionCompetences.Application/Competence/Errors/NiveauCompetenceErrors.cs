using GestionCompetences.Application.Common.Results;

namespace GestionCompetences.Application.Competence.Errors
{
    public class NiveauCompetenceErrors
    {
        public static Error NiveauCompetencesException => Error.Create("Niveau.Compétences.Exception", "Une erreur est survenue avec le serveur.");
        public static Error NiveauCompetencesLabelExiste => Error.Create("Niveau.Compétences.Label.Existe", "Le label est déjà utiliser.");
        public static Error NiveauCompetencesPasTrouver => Error.Create("Niveau.Compétences.Pas.Trouver", "La compétence n'a pas été trouver");
        public static Error NiveauCompetencesPasModifier => Error.Create("Niveau.Compétences.Pas.Modifier", "Le nom du label n'a pas changé");
    }
}
