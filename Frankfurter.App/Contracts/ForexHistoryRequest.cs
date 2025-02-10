using Frankfurter.App.Validators;
using System.ComponentModel.DataAnnotations;

namespace Frankfurter.App.Contracts;

[ForexHistoryRequestValidation]
public class ForexHistoryRequest
{
    [Required]
    public DateOnly StartDate { get; set; } 

    public DateOnly? EndDate { get; set; }
}
