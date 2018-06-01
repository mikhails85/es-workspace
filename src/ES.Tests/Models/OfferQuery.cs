using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;

namespace ES.Tests.Models
{
    public class OfferQuery
    {
        public string Id { get; set; }        
        public QueryContainer Query { get; set; }
    }
}