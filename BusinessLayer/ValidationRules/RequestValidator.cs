using DataAccessLayer.Models;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
           
            RuleFor(x => x.Request_Subject).NotEmpty().WithMessage("You cannot send the message without entering subject");
            RuleFor(x => x.Request_Subject).MinimumLength(3).WithMessage("Please enter more than 3 character");
            RuleFor(x => x.Request_Subject).MaximumLength(100).WithMessage("Please enter less than 100 character");
            
           

 
        }
    }
}
