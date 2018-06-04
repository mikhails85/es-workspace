using System;

namespace Dapper.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
           // (new DapperRepository()).AddSkill("C#");
            
            var result = (new DapperRepository()).GetSkills();
            
            foreach(var skill in result)
            {                
                Console.WriteLine($"Name:{skill.Name}");
            }
            
        }
    }
}
