using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDeck.Application.Infrastructure.Identity;
using FlightDeck.Application.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDeck.Api.Controllers
{
    [Route("api/account")] //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            var userAgent = Request.Headers["User-Agent"];
            var ipAddress = "";
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                ipAddress = Request.Headers["X-Forwarded-For"];
            else
                ipAddress= HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            return Ok(await _authenticationService.AuthenticateAsync(request , userAgent, ipAddress));
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _authenticationService.RegisterAsync(request, origin));
        }
        
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _authenticationService.ConfirmEmailAsync(userId, code));
        }
       
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {            
            return Ok(await _authenticationService.ForgotPasswordAsync(model, Request.Headers["origin"]));
        }
       
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {

            return Ok(await _authenticationService.ResetPasswordAsync(model));
        }
    }
}
