using FluentValidation;
using TaskScheduler.Core.Constants;
using TaskScheduler.Core.Models.CreateUpdate;

namespace TaskScheduler.Code.Validators
{
    public class WorkTaskValidator : AbstractValidator<WorkTaskCreateUpdateModel>
    {
        public WorkTaskValidator()
        {
            RuleFor(p => p.Title).NotEmpty().MaximumLength(FieldConstants.Lenght250);
            RuleFor(p => p.DueDate).Must((d) => d >= DateTime.Now);
        }
    }
}
