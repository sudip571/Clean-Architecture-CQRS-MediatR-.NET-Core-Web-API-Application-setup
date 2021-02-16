using FlightDeck.Application.Enums;
using FlightDeck.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.Identity.Seeds
{
    /// <summary>
    /// when you migrate database for the first time, Identity Core will generate all the
    /// required table in database
    /// if you want to create some user and assign him some role during database migration
    /// then this is what you need to do
    /// </summary>
    public static class BasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                FirstName = "Flight",
                LastName = "Deck",
                UserName = "FlightDeckUser",
                Email = "flightdeck@test.com",               
                EmailConfirmed = true
                
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Flightdeck@123");
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
            }

            //if (userManager.Users.All(u => u.Id != defaultUser.Id))
            //{
            //    var user = await userManager.FindByEmailAsync(defaultUser.Email);
            //    if (user == null)
            //    {
            //        await userManager.CreateAsync(defaultUser, "flightdeck@123");
            //        await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
            //    }

            //}
        }
    }
}
