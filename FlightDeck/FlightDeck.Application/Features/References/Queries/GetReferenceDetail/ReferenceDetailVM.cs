﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.Features.References.Queries.GetReferenceDetail
{
   public  class ReferenceDetailVM
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public bool IsLocationSafe { get; set; }
        public string Person { get; set; }
        public string Disease { get; set; }
    }
}
