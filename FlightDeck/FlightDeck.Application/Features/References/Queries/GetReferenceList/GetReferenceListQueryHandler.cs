using AutoMapper;
using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.JsonModels;
using FlightDeck.Application.Responses;
using FlightDeck.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightDeck.Application.Features.References.Queries.GetReferenceList
{
    public class GetReferenceListQueryHandler : IRequestHandler<GetReferenceListQuery, Response<List<ReferenceListVM>>>
    {
        private readonly IAsyncReferenceRepository _referenceRepository;
        private readonly IMapper _mapper;
        private readonly ConnectionStrings _connectionStrings;
        public GetReferenceListQueryHandler(IAsyncReferenceRepository referenceRepository, IMapper mapper, IOptions<ConnectionStrings> connectionStrings)
        {
            _referenceRepository = referenceRepository;
            _mapper = mapper;
            _connectionStrings = connectionStrings.Value;
        }
        public async  Task<Response<List<ReferenceListVM>>> Handle(GetReferenceListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ReferenceListVM>>();
            var result = await _referenceRepository.GetAllAsync();
            var mappedResult = _mapper.Map<List<ReferenceListVM>>(result);
            response.Data = mappedResult;
            return response;
        }
    }
}
