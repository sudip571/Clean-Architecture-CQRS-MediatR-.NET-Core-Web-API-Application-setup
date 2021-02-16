using FlightDeck.Application.Infrastructure.Service.RandomToken;
using PasswordGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Service.RandomToken
{
    public class TokenGenerator : ITokenGenerator
    {
        public string StrongPassword(int length = 8)
        {
            var pwd = new Password(length).IncludeLowercase().IncludeUppercase().IncludeSpecial().IncludeNumeric();
            return pwd.Next();
        }
        public string FourDigitPin(int length = 4)
        {
            var pwd = new Password(length).IncludeNumeric();
            return pwd.Next();
        }
    }
}
