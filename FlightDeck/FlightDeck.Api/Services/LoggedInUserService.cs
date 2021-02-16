using FlightDeck.Application.Api;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightDeck.Api.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Email);
        }

        public string UserId { get; }
        public string Email { get; }
        
    }
}
