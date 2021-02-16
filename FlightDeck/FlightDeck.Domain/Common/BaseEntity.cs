using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Domain.Common
{
    /// <summary>
    /// Common properties shared by other Database Entities
    /// </summary>
   public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
    }
}
