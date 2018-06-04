using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dapper.Tests.Models
{
    public class Offer
    {
        public int Id {get;set;}        
        public string Title {get;set;}
        public string Description {get;set;}
        public List<Skill> Skills {get;set;}
    }
}