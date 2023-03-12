using DDD.Utilities.DTOs.ContactDependents;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Utilities.DTOs.Contact
{
    public class UpdateContactDTO : ContactDto
    {        
    }

    public class UpdatedContactDtoValidator : AbstractValidator<UpdateContactDTO>
    {
        public UpdatedContactDtoValidator()
        {
            //RuleFor(x => x.Id)
            //        .NotEmpty()
            //        .WithMessage("No contact for updating was received.");

            RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("No name for updating contact was provided.");
        }
    }
}
