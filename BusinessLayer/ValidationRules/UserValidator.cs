using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using FluentValidation;
using System;
using System.Linq;

namespace BusinessLayer.ValidationRules
{
    public class UserValidator : AbstractValidator<Tbl_Users> 
    {
        public UserValidator()
        {
            UserManager um = new UserManager();

            RuleFor(x => x.Name_Surname).NotEmpty().WithMessage("You cannot skip without entering data").Length(2,20).WithMessage("Please enter between 2 - 20");
            
            RuleFor(x => x.password).NotEmpty().WithMessage("You cannot skip without entering data").Length(5, 15).WithMessage("Please enter between 5 - 15");
            RuleFor(x => x.Email).NotEmpty().WithMessage("You cannot skip without entering data").EmailAddress().WithMessage("Please enter valid email address");
            RuleFor(x => x.TelefonNo).NotEmpty().WithMessage("You cannot skip without entering data").Length(10).WithMessage("Please enter valid number");
            RuleFor(x => x.Sube).NotEmpty().WithMessage("You cannot skip without entering data").Length(2, 20).WithMessage("Please enter between 2 - 20");
            //RuleFor(x => x.country).NotEmpty().WithMessage("You cannot skip without entering data").Length(2, 20).WithMessage("Please enter between 2 - 20");
            //RuleFor(x => x.Email).Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").WithMessage("Not a valid email").Matches(Convert.ToString(um.GetUsers().FirstOrDefault())).WithMessage("This mail is using");
            //RuleFor(x => x.User_Mail).NotEqual(Convert.ToString(um.GetUsers().FirstOrDefault())).WithMessage("This mail is using");
            //var UserValues = um.GetUsers();
            //RuleFor(x => x.Department_Id).NotNull().NotEmpty().WithMessage("You cannot skip without entering data");
            //RuleFor(x => x.User_Name).MinimumLength(3).WithMessage("Please enter more than 2 character");
            //RuleFor(x => x.User_Name).MaximumLength(50).WithMessage("Please enter less than 50 character");
            //RuleFor(x => x.User_Surname).MinimumLength(3).WithMessage("Please enter more than 3 character");
            //RuleFor(x => x.User_Surname).MaximumLength(50).WithMessage("Please enter less than 50 character");
            //RuleFor(x => x.User_Password).MinimumLength(3).WithMessage("Please enter more than 3 character");
            //RuleFor(x => x.User_Password).MaximumLength(15).WithMessage("Please enter less than 15 character");
            //RuleFor(x => x.User_Phone).MinimumLength(10).WithMessage("Please enter 10 character");
            //RuleFor(x => x.User_Phone).MaximumLength(10).WithMessage("Please enter 10 character");
            //RuleFor(x => x.Department_Description).MaximumLength(250).WithMessage("Please enter less than 250 character");

        }
    }
}
