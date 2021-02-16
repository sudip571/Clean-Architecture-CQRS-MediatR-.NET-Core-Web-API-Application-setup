using FlightDeck.Application.Infrastructure.Identity.Models;
using FlightDeck.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.Application.Infrastructure.Identity
{
    public interface IAuthenticationService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request,string userAgent,string ipAddress);
        Task<Response<string>> RegisterAsync(RegistrationRequest request, string origin);
        Task<Response<string>> ForgotPasswordAsync(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPasswordAsync(ResetPasswordRequest model);
        Task<Response<string>> ChangePasswordAsync(ChangePasswordRequest model);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
    }
}
