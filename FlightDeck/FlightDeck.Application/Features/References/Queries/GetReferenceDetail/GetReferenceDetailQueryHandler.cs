using AutoMapper;
using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightDeck.Application.Features.References.Queries.GetReferenceDetail
{
    public class GetReferenceDetailQueryHandler : IRequestHandler<GetReferenceDetailQuery, Response<ReferenceDetailVM>>
    {
        private readonly IAsyncReferenceRepository _referenceRepository;
        private readonly IMapper _mapper;

        public GetReferenceDetailQueryHandler(IAsyncReferenceRepository referenceRepository, IMapper mapper)
        {
            _referenceRepository = referenceRepository;
            _mapper = mapper;
        }
        public async Task<Response<ReferenceDetailVM>> Handle(GetReferenceDetailQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<ReferenceDetailVM>();
            var result = await _referenceRepository.GetByIdAsync(request.Id);
            var mappedResult = _mapper.Map<ReferenceDetailVM>(result);
            response.Data = mappedResult;
            return response;
        }
    }
}
