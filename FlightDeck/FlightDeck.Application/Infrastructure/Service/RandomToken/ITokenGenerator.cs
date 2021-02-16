using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.Infrastructure.Service.RandomToken
{
    public interface ITokenGenerator
    {
        string StrongPassword(int length);
        string FourDigitPin(int length);
    }
}
