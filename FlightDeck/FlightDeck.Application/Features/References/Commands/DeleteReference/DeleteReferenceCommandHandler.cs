using AutoMapper;
using FlightDeck.Application.Exceptions;
using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightDeck.Application.Features.References.Commands.DeleteReference
{
    public  class DeleteReferenceCommandHandler : IRequestHandler<DeleteReferenceCommand, Response<int>>
    {
        private readonly IAsyncReferenceRepository _referenceRepository;
        private readonly IMapper _mapper;
        public DeleteReferenceCommandHandler(IAsyncReferenceRepository referenceRepository, IMapper mapper)
        {
            _referenceRepository = referenceRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeleteReferenceCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<int>();
            var modelToDelete = await _referenceRepository.GetByIdAsync(request.Id);

            if (modelToDelete == null)
            {
                throw new NotFoundException($"{request.Id} is not found");
            }
            await _referenceRepository.DeleteAsync(request.Id);
            response.Message = "Deleted successfully";
            return response;
        }
    }
}
