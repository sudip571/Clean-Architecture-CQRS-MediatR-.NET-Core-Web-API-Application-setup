using AutoMapper;
using FlightDeck.Application.Exceptions;
using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.Responses;
using FlightDeck.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightDeck.Application.Features.References.Commands.UpdateReference
{
    public class UpdateReferenceCommandHandler : IRequestHandler<UpdateReferenceCommand, Response<int>>
    {
        private readonly IAsyncReferenceRepository _referenceRepository;
        private readonly IMapper _mapper;
        public UpdateReferenceCommandHandler(IAsyncReferenceRepository referenceRepository, IMapper mapper)
        {
            _referenceRepository = referenceRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdateReferenceCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<int>();
            var modelToUpdate = await _referenceRepository.GetByIdAsync(request.Id);

            if (modelToUpdate == null)
            {
                throw new NotFoundException($"{request.Person} is not found");
            }
            // Execute a mapping from the source object to existing destination object
            _mapper.Map(request, modelToUpdate, typeof(UpdateReferenceCommand), typeof(Reference));
            await _referenceRepository.UpdateAsync(modelToUpdate);
            response.Message = "Updated successfully";
            return response;
        }
    }
}
