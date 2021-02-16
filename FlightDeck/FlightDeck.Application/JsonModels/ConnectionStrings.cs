using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.JsonModels
{
    public class ConnectionStrings
    {
        public string Postgresql { get; set; }       
        public string FlightDeckAPI { get; set; }
        public string IdentityConnection { get; set; }
    }
}
