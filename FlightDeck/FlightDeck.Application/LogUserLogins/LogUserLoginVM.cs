﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.LogUserLogins
{
    public class LogUserLoginVM
    {
        public int LoginAttemptCount { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime LoginAttemptDate { get; set; }
        public string IpAddress { get; set; }
        public string ClientInfo { get; set; }
        public string OSInfo { get; set; }
        public string Device { get; set; }
        public string  DeviceBrand { get; set; }
        public string DeviceModel { get; set; }
    }
}
