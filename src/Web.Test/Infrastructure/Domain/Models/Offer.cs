using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Domain.Models
{
    public class Offer
    {
        public string Name {get; set;}
        public string Description{get; set;}
        public List<Skill> RequaredSkills {get; set;}
    }
}