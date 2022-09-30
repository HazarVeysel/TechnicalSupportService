using DataAccessLayer.Models;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Message_Content).NotEmpty().WithMessage("You cannot send blank message, please describe your request");
            RuleFor(x => x.Request.Request_Subject).NotEmpty().WithMessage("You cannot send the message without entering subject").Length(2, 100).WithMessage("Please enter between 2 - 100");
            RuleFor(x => x.Request.Request_Subject).MinimumLength(3).WithMessage("Please enter more than 3 character");
            RuleFor(x => x.Request.Request_Subject).MaximumLength(100).WithMessage("Please enter less than 100 character");
            
           


        }
    }
}
