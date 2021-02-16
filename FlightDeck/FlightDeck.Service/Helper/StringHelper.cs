using FlightDeck.Application.Infrastructure.Service.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FlightDeck.Service.Helper
{
    public class StringHelper : IStringHelper
    {
        public string RemoveSpaceFromString(string sentence)
        {
            return Regex.Replace(sentence, @"\s+", " ");
        }
    }
}
