using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDeck.Application.Enums;
using FlightDeck.Application.Features.References.Commands.CreateReference;
using FlightDeck.Application.Features.References.Commands.DeleteReference;
using FlightDeck.Application.Features.References.Commands.UpdateReference;
using FlightDeck.Application.Features.References.Queries.GetReferenceDetail;
using FlightDeck.Application.Features.References.Queries.GetReferenceList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDeck.Api.Controllers
{
    [ApiVersion("1.0")]   
    [Authorize]
    public class ReferenceController : BaseApiController
    {
        //private readonly IMediator _mediator;

        //public ReferenceController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        // GET: api/reference

        [HttpGet]       
        public async Task<IActionResult> Get()
        {
            var requestObject = new GetReferenceListQuery();
            var response = await _mediator.Send(requestObject);
            return Ok(response);

        }

        // GET api/reference/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> Get(int id)
        {
            var requestObject = new GetReferenceDetailQuery()
            {
                Id = id
            };
            var response = await _mediator.Send(requestObject);
            return Ok(response);
        }

        // POST api/reference
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReferenceCommand createReferenceCommand)
        {
            var response = await _mediator.Send(createReferenceCommand);
            return Ok(response);
        }

        // PUT api/reference/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateReferenceCommand updateReferenceCommand)
        {
            if (id != updateReferenceCommand.Id)
            {
                return BadRequest();
            }
            var response = await _mediator.Send(updateReferenceCommand);
            return Ok(response);
           
        }

        // DELETE api/reference/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var requestObject = new DeleteReferenceCommand()
            {
                Id = id
            };
            var response = await _mediator.Send(requestObject);
            return Ok(response);
        }
    }
}
