using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class CollectionsTest
    {
        [Test]
        public void StringList_CreateMany_ByDefaultCreates3Items()
        {
            var fixture = new Fixture();
            
            IEnumerable<string> myStrings = fixture.CreateMany<string>();

            Assert.That(myStrings.Count(), Is.EqualTo(3));
        }
        [Test]
        public void IntList_CreateMaySpecifyQuantity_Works()
        {
            var fixture = new Fixture();

            var ints = fixture.CreateMany<int>(23);

            Assert.That(ints.Count(), Is.EqualTo(23));
            Assert.That(ints, Is.InstanceOf<IEnumerable<int>>());
        }

        [Test]
        public void AddManyTo_AddingToExistingCollection_IsEvenSimpler()
        {
            var fixture = new Fixture();
            var myList = new List<Exception> { new Exception("1"), new Exception("2"), new Exception("3") };

            fixture.AddManyTo(myList, 12);

            Assert.That(myList, Has.Property("Count").EqualTo(15));
        }

        [Test]
        public void AddManyTo_Creator_CanBeModified()
        {
            var fixture = new Fixture();
            var myStrings = new List<char> {'a','b','c' };
            
            fixture.AddManyTo(myStrings, ()=>'z');

            Assert.That(myStrings, Has.Property("Count").EqualTo(6));
            Assert.That(myStrings, Has.Exactly(1).EqualTo('a'));
            Assert.That(myStrings, Has.Exactly(1).EqualTo('b'));
            Assert.That(myStrings, Has.Exactly(1).EqualTo('c'));
            Assert.That(myStrings, Has.Exactly(3).EqualTo('z'));
             
            var startChar = 'd';
            fixture.AddManyTo(myStrings, ()=> { return startChar++; });
            Assert.That(myStrings, Has.Exactly(1).EqualTo('d'));
            Assert.That(myStrings, Has.Exactly(1).EqualTo('e'));
            Assert.That(myStrings, Has.Exactly(1).EqualTo('f'));
        }
    }
}
