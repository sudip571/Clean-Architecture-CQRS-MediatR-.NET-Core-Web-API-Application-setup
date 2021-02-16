using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.Application.LogUserLogins
{
    public interface ILogUserLoginService
    {
        Task AddUserLoginLogAsync(string userId, string userName, int attemptCount, string userAgent, string ipAddress);
    }
}
