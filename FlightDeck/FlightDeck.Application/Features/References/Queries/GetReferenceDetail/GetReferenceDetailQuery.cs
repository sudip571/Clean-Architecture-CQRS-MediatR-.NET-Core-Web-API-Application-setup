using FlightDeck.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.Features.References.Queries.GetReferenceDetail
{
    public class GetReferenceDetailQuery :IRequest<Response<ReferenceDetailVM>>
    {
        public int Id { get; set; }
    }
}
