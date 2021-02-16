using FlightDeck.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.Features.References.Commands.DeleteReference
{
   public class DeleteReferenceCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
}
