using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.Application.Infrastructure.Service.Email
{
   public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
