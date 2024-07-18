using FluentValidation;
using TaskScheduler.Core.Constants;
using TaskScheduler.Core.Models.CreateUpdate;

namespace TaskScheduler.Code.Validators
{
    public class UserValidator : AbstractValidator<UserCreateUpdateModel>
    {
        public UserValidator() {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(FieldConstants.Lenght150);

        }
    }
}
