using AutoMapper;
using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.Responses;
using FlightDeck.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightDeck.Application.Features.References.Commands.CreateReference
{
   public  class CreateReferenceCommandHandler : IRequestHandler<CreateReferenceCommand, Response<int>>
    {
        private readonly IAsyncReferenceRepository _referenceRepository;
        private readonly IMapper _mapper;
        public CreateReferenceCommandHandler(IAsyncReferenceRepository referenceRepository, IMapper mapper)
        {
            _referenceRepository = referenceRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateReferenceCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<int>();
            //Execute a mapping from the source object to a new destination object.
            var model = _mapper.Map<Reference>(request);
            await _referenceRepository.AddAsync(model);
            response.Message = "Data added successfully";
            return response;
        }
    }
}
