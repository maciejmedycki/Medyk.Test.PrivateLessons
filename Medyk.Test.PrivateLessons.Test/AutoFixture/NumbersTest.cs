using AutoFixture;
using Medyk.Test.PrivateLessons.AutoFixture;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    //https://github.com/AutoFixture/AutoFixture/wiki/Cheat-Sheet

    [TestFixture]
    public class NumbersTest
    {
        [Test]
        public void Test()
        {
            var fixture = new Fixture();
            var sut = new Numbers();
            var a = fixture.Create<int>();
            var b = fixture.Create<int>();//same for all others numbers in C#

            var result = sut.Multiply(a,b);

            Assert.That(result, Is.EqualTo(a*b));
        }
    }
}