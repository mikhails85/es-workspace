using System;
using ES.Tests.Tests;

namespace ES.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var simpleTest = new SimpleTest();
            simpleTest.AggregationsTest(); 
        }
    }
}
