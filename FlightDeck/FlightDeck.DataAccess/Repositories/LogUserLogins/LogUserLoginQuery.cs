using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.DataAccess.Repositories.LogUserLogins
{
    public static class LogUserLoginQuery
    {
        // SQL Table Names
        public const string INSERT = @"INSERT INTO LogUserLogin(UserId, UserName, LoginAttemptCount,IpAddress,ClientInfo,OSInfo,Device,DeviceBrand,DeviceModel,LoginAttemptDate)
                                     OUTPUT INSERTED.*
                                     VALUES(@UserId, @UserName, @LoginAttemptCount,@IpAddress,@ClientInfo,@OSInfo,@Device,@DeviceBrand,@DeviceModel,@LoginAttemptDate)";


    }
}
