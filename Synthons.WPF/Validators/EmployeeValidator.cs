using FluentValidation;
using Synthons.Domain;

namespace Synthons.WPF.Validators;
public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.BirthDate).LessThanOrEqualTo(DateTime.Now.AddYears(-16));
    }
}
