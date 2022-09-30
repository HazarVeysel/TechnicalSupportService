using DataAccessLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Department_Name).NotEmpty().WithMessage("You cannot skip without entering data");
            RuleFor(x => x.Department_Description).NotEmpty().WithMessage("You cannot skip without entering data");
            RuleFor(x => x.Department_Name).MinimumLength(3).WithMessage("Please enter more than 3 character");
            RuleFor(x => x.Department_Name).MaximumLength(50).WithMessage("Please enter less than 50 character");
            RuleFor(x => x.Department_Description).MaximumLength(250).WithMessage("Please enter less than 250 character");

        }
    }
}
