using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.Api
{
    public  interface ILoggedInUserService
    {
         string UserId { get; }
         string Email { get; }
    }
}
