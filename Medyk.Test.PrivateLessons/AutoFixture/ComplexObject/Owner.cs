using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Medyk.Test.PrivateLessons.AutoFixture.ComplexObject
{
    public class Owner
    {
        public bool IsCompany { get; set; }
        public DateTime LastChange { get; set; }

        [StringLength(10)]
        public string Name { get; set; }
        public IEnumerable<string> Notes { get; set; }
    }
}