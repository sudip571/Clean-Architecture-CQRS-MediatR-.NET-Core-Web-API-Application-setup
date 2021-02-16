using FlightDeck.Application.Enums;
using FlightDeck.Application.Infrastructure.Identity;
using FlightDeck.Application.Infrastructure.Identity.Models;
using FlightDeck.Application.Infrastructure.Service.Email;
using FlightDeck.Application.JsonModels;
using FlightDeck.Application.LogUserLogins;
using FlightDeck.Application.Responses;
using FlightDeck.Identity.Helper;
using FlightDeck.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ILogUserLoginService _logUserLoginService;
        private readonly JwtSettings _jwtSettings;
        private readonly MailSettings _mailSettings;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IOptions<JwtSettings> jwtSettings, IOptions<MailSettings> mailSettings, SignInManager<ApplicationUser> signInManager,IEmailService emailService , ILogUserLoginService logUserLoginService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;         
            _signInManager = signInManager;
            _emailService = emailService;
            _logUserLoginService = logUserLoginService;
            _mailSettings = mailSettings.Value;
        }
       
        
        public async  Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request,string userAgent,string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"No Accounts Registered with {request.Email}.");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, true, lockoutOnFailure: true);
            //keeping log for user login activity
            await _logUserLoginService.AddUserLoginLogAsync(user.Id,user.UserName,user.AccessFailedCount,userAgent,ipAddress);
            if (!result.Succeeded)
            {
                int maxAttempt = 5;
                int attemptLeft = maxAttempt - user.AccessFailedCount;
                var exceptionMessage = $"Invalid Credentials for '{request.Email}'.";
                if (attemptLeft > 0)
                    exceptionMessage = exceptionMessage + $" Retry attempt left : {attemptLeft}";
                if (attemptLeft == maxAttempt)
                    exceptionMessage = "Your account has been locked";

                throw new Exception(exceptionMessage);
            }
            //Uncomment the following line of code, if the system has Account verified feature
            //if (!user.EmailConfirmed)
            //{
            //    throw new Exception($"Account Not Confirmed for '{request.Email}'.");
            //}
            var token = await TokenHelper.GenerateJWToken(user, _userManager, _jwtSettings);
            var rolesList = (await _userManager.GetRolesAsync(user)).ToList();
            //var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var response = new AuthenticationResponse()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = rolesList,
                Token = token
            };
            return new Response<AuthenticationResponse>("Authenticated successfully",true,response);
        }
       
        /// <summary>
        /// Origin refers to the website host url e.g. Origin: https://developer.mozilla.org
        /// </summary>
        /// <param name="request"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public async  Task<Response<string>> RegisterAsync(RegistrationRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new Exception($"Username '{request.UserName}' is already taken.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,               
                UserName = request.UserName
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    // uncomment below line if you need to assign role to newly requested user.
                    // you may get role from form/frontend side
                    //await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                    var verificationUri = await SendVerificationEmail(user, origin);                    
                    var emailRequest = new EmailRequest()
                    {
                        From = "ab.@test.com",//_mailSettings.EmailFrom
                        To = user.Email,
                        Subject = "Confirm FlightDeck Registration",
                        Body = $"Please confirm your account by visiting this URL {verificationUri}   Thank you"
                    };
                    await _emailService.SendAsync(emailRequest);
                    return new Response<string>("Confirmation link has been sent to your email",true, "Confirmation link has been sent to your email.Please visit the link to verify your account");
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email } is already registered.");
            }
        }

      
        /// <summary>
        /// Steps:
        /// ResetToken is generated and sent to user's email.
        /// you can also sent frontend link that opens  reset-form having atleast Email,Token and New-password field.        
        /// </summary>
        /// <param name="model"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public async Task<Response<string>> ForgotPasswordAsync(ForgotPasswordRequest model, string origin)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);          
            if (user == null)
                throw new Exception($"No Accounts Registered with {model.Email}.");
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var resetPasswordRoute = "api/account/reset-password/";
            //var completeResetPasswordRoute = new Uri(string.Concat($"{origin}/", resetPasswordRoute));           
            var emailRequest = new EmailRequest()
            {
                From = "ab.@gmail.com",//_mailSettings.EmailFrom
                To = model.Email,
                Subject = "Reset Password",
                Body = $"Your reset token is - {resetToken}"
            };
            await _emailService.SendAsync(emailRequest);
            return new Response<string>("Reset Password token has been sent to your email", true, "please use the token sent in your email to reset your password");

        }

        public async Task<Response<string>> ResetPasswordAsync(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null)
                throw new Exception($"No Accounts Registered with {model.Email}.");
            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
            if (result.Succeeded)
            {
                return new Response<string>("Password reset successfully",true,"your password has been reset successfully");
            }
            else
            {
                throw new Exception($"Error occured while reseting the password.");
            }
        }

        public async Task<Response<string>> ChangePasswordAsync(ChangePasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null)
                throw new Exception($"No Accounts Registered with {model.Email}.");
            var result = await _userManager.ChangePasswordAsync(account, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return new Response<string>("Password changed successfully", true, "your password has been changed successfully");
            }
            else
            {
                throw new Exception($"Error occured while changing the password.");
            }
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/confirm-email/";
            var completeConfirmEmailURL = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(completeConfirmEmailURL.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);           
            return verificationUri;
        }

        /// <summary>
        /// Email will be sent to user with confirmation link which contains two query keys UserId and Code. 
        /// when user clicks link, it hits our API endpoint(controller) which accepts values of keys(UserId and Code) as parameter.       
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                // you can also send the email with link that opens login page or redirect to frontend's login page from controller
                return new Response<string>($"Account Confirmed for {user.Email}", true, "Congratulation, your account has been verified in FlightDeck.");
            }
            else
            {
                throw new Exception($"An error occured while confirming {user.Email}.");
            }
        }

      
    }
}
