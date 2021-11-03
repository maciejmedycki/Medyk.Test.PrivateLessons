using AutoFixture.NUnit3;
using Medyk.Test.PrivateLessons.AutoFixture;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class NUnitAutoFixture
    {
        [Test]
        public void Number_MultiplyTwoPositivesut_ResultIsOk()
        {
            var sut = new Numbers();

            var result = sut.Multiply(2, 2);

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Number_MultiplyTwoNegativesut_ResultIsOk()
        {
            var sut = new Numbers();

            var result = sut.Multiply(-2, -2);

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Number_MultiplyTwoPosAndNegsut_ResultIsOk()
        {
            var sut = new Numbers();

            var result = sut.Multiply(-2, 2);

            Assert.That(result, Is.EqualTo(-4));
        }

        [Test]
        public void Number_MultiplyTwoNegAndPossut_ResultIsOk()
        {
            var sut = new Numbers();

            var result = sut.Multiply(2, -2);

            Assert.That(result, Is.EqualTo(-4));
        }

        //NUnit to the rescue
        [Test]
        [TestCase(2, 2, 4)]
        [TestCase(-2, -2, 4)]
        [TestCase(-2, 2, -4)]
        [TestCase(2, -2, -4)]
        public void Number_MultiplyNumbersNUnit_ResultIsOk(int a, int b, int c)
        {
            var sut = new Numbers();

            var result = sut.Multiply(a,b);

            Assert.That(result, Is.EqualTo(c));
        }

        //Add AutoFixture.NUnit3 nugpkg
        [Test]
        [AutoData]
        public void Number_MultiplyNumbersAutoNumber_ResultIsOk(int a, int b)
        {
            var sut = new Numbers();

            var result = sut.Multiply(a, b);

            Assert.That(result, Is.EqualTo(a*b));
        }

        //one step further SUT!
        [Test]
        [AutoData]
        public void Number_MultiplyNumbersAutoNumberAndSut_ResultIsOk(int a, int b, Numbers sut)
        {
            var result = sut.Multiply(a, b);

            Assert.That(result, Is.EqualTo(a * b));
        }

        //more control
        [Test]
        [InlineAutoData]//this is [TestCase] on AutoFixture steroids
        [InlineAutoData(0)]
        [InlineAutoData(-2)]
        [InlineAutoData(-2, -2)]
        public void Number_MultiplyNumbersSemiFullAuto_ResultIsOk(int a, int b, Numbers sut)
        {
           var result = sut.Multiply(a, b);

            Assert.That(result, Is.EqualTo(a * b));
        }
    }
}