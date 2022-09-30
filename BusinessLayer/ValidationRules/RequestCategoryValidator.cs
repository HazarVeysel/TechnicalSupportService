using DataAccessLayer.Models;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class RequestCategoryValidator : AbstractValidator<RequestCategory>
    {
        public RequestCategoryValidator()
        {
            RuleFor(x => x.RequestCategory_Name).NotEmpty().WithMessage("You cannot skip without entering data");
            RuleFor(x => x.Category_Description).NotEmpty().WithMessage("You cannot skip without entering data");
            RuleFor(x => x.RequestCategory_Name).MinimumLength(3).WithMessage("Please enter more than 3 character");
            RuleFor(x => x.RequestCategory_Name).MaximumLength(50).WithMessage("Please enter less than 50 character");
            RuleFor(x => x.Category_Description).MaximumLength(250).WithMessage("Please enter less than 250 character");

        }
    }
}
