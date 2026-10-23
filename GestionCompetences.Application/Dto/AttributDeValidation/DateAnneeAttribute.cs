using System.ComponentModel.DataAnnotations;

namespace GestionCompetences.Application.Dto.AttributDeValidation
{
    public class DateAnneeAttribute(int anneeMin) : ValidationAttribute
    {
        private readonly int _anneeMin = anneeMin;

        public override bool IsValid(object? value)
        {
            return value is DateTime date && date.Year >= _anneeMin;
        }
    }
}
