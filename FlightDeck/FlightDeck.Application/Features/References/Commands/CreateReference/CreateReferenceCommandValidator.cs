using FlightDeck.Application.Infrastructure.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightDeck.Application.Features.References.Commands.CreateReference
{
    public  class CreateReferenceCommandValidator : AbstractValidator<CreateReferenceCommand>
    {
        private readonly IAsyncReferenceRepository _referenceRepository;
        public CreateReferenceCommandValidator(IAsyncReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;

            RuleFor(p => p.Person)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 character")
               // .MustAsync(IsUniqueName).WithMessage("same person name already exist")
                ;
           
        }
        //private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
        //{

        //    return await _referenceRepository.IsUniqueNameAsync(name);
        //}
    }
}
