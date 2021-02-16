using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.Infrastructure.Identity.Models
{
   public class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
