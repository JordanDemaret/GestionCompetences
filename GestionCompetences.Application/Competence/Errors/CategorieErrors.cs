using GestionCompetences.Application.Common.Results;

namespace GestionCompetences.Application.Competence.Errors
{
    public class CategorieErrors
    {
        public static Error CategorieException => Error.Create("Catégorie.Exception", "Une erreur est survenue avec le serveur.");
        public static Error CategorieNomExiste => Error.Create("Catégorie.Nom.Existe", "Le nom est déjà utiliser.");
        public static Error CategoriePasTrouver => Error.Create("Catégorie.Pas.Trouver", "La catégorie n'a pas été trouver");
        public static Error CategoriePasModifier => Error.Create("Catégorie.Pas.Modifier", "La nom de la catégorie est le même.");
    }
}
