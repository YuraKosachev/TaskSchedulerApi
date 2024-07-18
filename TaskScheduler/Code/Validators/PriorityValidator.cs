using FluentValidation;
using TaskScheduler.Core.Constants;
using TaskScheduler.Core.Models.CreateUpdate;

namespace TaskScheduler.Code.Validators
{
    public class PriorityValidator : AbstractValidator<PriorityCreateUpdateModel>
    {
        public PriorityValidator()
        {
            RuleFor(p => p.Title).NotEmpty().MaximumLength(FieldConstants.Lenght50);
        }
    }
}
