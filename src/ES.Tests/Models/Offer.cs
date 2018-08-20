using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;
namespace ES.Tests.Models
{
    public class Offer
    {
        public int Index{get;set;}
        public string Title {get;set;}
        public string Description {get;set;}    
        public List<Skill> Skills {get;set;}
    }
    
    public class Skill {
        public int Id {get;set;}
        public string Name {get;set;}
    }
}