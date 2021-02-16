using FlightDeck.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Identity.Contexts
{
    public class FlightDeckIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public FlightDeckIdentityContext(DbContextOptions<FlightDeckIdentityContext> options) : base(options)
        {
        }
    }
}
