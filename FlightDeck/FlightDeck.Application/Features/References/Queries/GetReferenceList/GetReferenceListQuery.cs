using FlightDeck.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.Features.References.Queries.GetReferenceList
{
   public class GetReferenceListQuery :IRequest<Response<List<ReferenceListVM>>>
    {
    }
}
