using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.Application.Dto.AttributDeValidation
{
    /// <summary>
    /// Attribut de validation vérifiant qu'une date respecte une année minimale.
    /// </summary>
    /// <param name="anneeMin">L'année minimale autorisée.</param>
    public class DateAnneeAttribute(int anneeMin) : ValidationAttribute
    {
        private readonly int _anneeMin = anneeMin;

        /// <summary>
        /// Valide si la valeur fournie est une date dont l'année est supérieure ou égale à l'année minimale requise.
        /// </summary>
        /// <param name="value">La valeur de la date à valider.</param>
        /// <returns><c>true</c> si la date est valide et respecte l'année minimale, sinon <c>false</c>.</returns>
        public override bool IsValid(object? value)
        {
            return value is DateTime date && date.Year >= _anneeMin;
        }
    }
}
