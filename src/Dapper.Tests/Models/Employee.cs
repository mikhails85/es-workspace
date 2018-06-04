using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dapper.Tests.Models
{
    public class Employee
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string CV{get;set;}
        public List<Skill> Skills {get;set;}
    }
}