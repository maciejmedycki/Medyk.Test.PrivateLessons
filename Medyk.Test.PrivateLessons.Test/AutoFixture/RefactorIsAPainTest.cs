using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    public class SimpleDto 
    {
        //TODO: add new property
        public SimpleDto(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public int Value { get; private set; }
    }

    [TestFixture]
    public class RefactorIsAPainTest
    {
        [Test]
        public void ManualTest_Refactor_IsPain()
        {
            var dto1 = new SimpleDto("test1", 12);
            var dto2 = new SimpleDto("test2", 134);
            var dto3 = new SimpleDto("test3", 121);

            var list = new List<SimpleDto> { dto1, dto2, dto3};

            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void AnonymousDataTest_Refactor_NotSoBad()
        {
            var fixture = new Fixture();

            var list = fixture.CreateMany<SimpleDto>(3).ToList();

            Assert.That(list, Has.Count.EqualTo(3));
        }
    }
}