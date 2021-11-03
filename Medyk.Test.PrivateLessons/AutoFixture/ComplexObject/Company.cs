using System;
using System.Collections.Generic;

namespace Medyk.Test.PrivateLessons.AutoFixture.ComplexObject
{
    public class Company
    {
        public DateTime Established { get; set; }
        public bool IsGreat { get; set; }
        public string Name { get; set; }
        public List<Owner> Owners { get; set; }
    }
}