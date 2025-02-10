using Frankfurter.App.Validators;
using System.ComponentModel.DataAnnotations;

namespace Frankfurter.App.Contracts;

[ForexHistoryRequestValidation]
public class ForexHistoryRequest
{
    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "The startDate must be in the format YYYY-mm-dd.")]
    public string StartDate { get; set; } = string.Empty;

    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "The endDate must be in the format YYYY-mm-dd.")]
    public string? EndDate { get; set; }

    public string GetDateRange()
    {
        return StartDate + ".." + (EndDate ?? "");
    }
}
