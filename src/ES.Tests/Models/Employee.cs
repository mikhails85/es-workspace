using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ES.Tests.Models
{
    public class Employee
    {
        public string Name {get;set;}
        public string CV{get;set;}
        public List<string> Skills {get;set;}
    }
}