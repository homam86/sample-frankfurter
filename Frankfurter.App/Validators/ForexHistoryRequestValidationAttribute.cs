using Frankfurter.App.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Frankfurter.App.Validators;

public class ForexHistoryRequestValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var model = (ForexHistoryRequest)validationContext.ObjectInstance;
        if (model.EndDate.HasValue && model.StartDate > model.EndDate)
        {
            return new ValidationResult("StartDate must be earlier than EndDate.");
        }

        return ValidationResult.Success;
    }
}
