using AutoFixture;
using Medyk.Test.PrivateLessons.AutoFixture;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class StringsTest
    {
        [Test]
        public void JoinWithSpace_OldWay_ReturnsProperString()
        {
            var s1 = "one";
            var s2 = "two";
            var s3 = "three";
            var sut = new Strings();            

            var result = sut.JoinWithSpace(s1, s2, s3);

            Assert.That(result, Is.EqualTo("one two three"));
        }


        [Test]
        public void JoinWithSpace_FixtureSimple_ReturnsProperString_ButMoreWriting()
        {
            var fixture = new Fixture();
            var s1 = fixture.Create<string>();
            var s2 = fixture.Create<string>();
            var s3 = fixture.Create<string>();
            var sut = new Strings();

            var result = sut.JoinWithSpace(s1, s2, s3);

            //Assert.That(result, Is.EqualTo("one two three"));//nope
            Assert.That(result, Is.EqualTo($"{s1} {s2} {s3}"));
        }

        [Test]
        public void JoinWithSpace_FixtureSimple_ReturnsProperString_WithSeedNicerValues()
        {
            var fixture = new Fixture();
            var s1 = fixture.Create<string>("1_");//install AutoFixture.SeedExtensions
            var s2 = fixture.Create<string>("2_");
            var s3 = fixture.Create<string>("3_");
            var sut = new Strings();

            var result = sut.JoinWithSpace(s1, s2, s3);

            Assert.That(result, Is.EqualTo($"{s1} {s2} {s3}"));
            Assert.That(result, Is.Null);//let's fail and see the exception message
        }
    }
}