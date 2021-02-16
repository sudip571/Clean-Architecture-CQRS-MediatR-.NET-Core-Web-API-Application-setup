using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.JsonModels
{
    public class MailSettings
    {
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string DisplayName { get; set; }
    }
}
