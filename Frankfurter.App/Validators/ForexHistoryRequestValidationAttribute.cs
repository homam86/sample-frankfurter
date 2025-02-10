using Frankfurter.App.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Frankfurter.App.Validators;

public class ForexHistoryRequestValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var model = (ForexHistoryRequest)validationContext.ObjectInstance;
        if (!string.IsNullOrEmpty(model.EndDate) && string.Compare(model.StartDate, model.EndDate) >= 0)
        {
            return new ValidationResult("StartDate must be earlier than EndDate.");
        }

        return ValidationResult.Success;
    }
}
