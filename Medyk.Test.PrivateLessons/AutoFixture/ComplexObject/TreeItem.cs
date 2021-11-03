using System.Collections.Generic;

namespace Medyk.Test.PrivateLessons.AutoFixture.ComplexObject
{
    public class TreeItem
    {
        public IEnumerable<TreeItem> Children { get; set; }
        public Company Company { get; set; }

        public string Name { get; set; }
    }
}