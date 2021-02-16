using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Identity.Models
{
    /// <summary>
    /// Add all other extra  properties that you need like FirstName,LastName and so on.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
