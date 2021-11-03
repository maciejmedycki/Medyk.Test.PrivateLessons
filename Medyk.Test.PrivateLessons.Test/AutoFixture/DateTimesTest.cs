using System;
using AutoFixture;
using Medyk.Test.PrivateLessons.AutoFixture;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class DateTimesTest
    {
        [Test]
        public void AtNoon_AnonymousData_ReturnsNoon()
        {
            var fixture = new Fixture();
            var dateTime = fixture.Create<DateTime>();
            var sut = new DateTimes();

            var result = sut.AtNoon(dateTime, fixture.Create<string>(), fixture.Create<int>());//you can just use default(int) or 0 also, why not?

            Assert.That(result, Has.Property("Year").EqualTo(dateTime.Year));
            Assert.That(result, Has.Property("Month").EqualTo(dateTime.Month));
            Assert.That(result, Has.Property("Day").EqualTo(dateTime.Day));
            Assert.That(result, Has.Property("Hour").EqualTo(12));
            Assert.That(result, Has.Property("Minute").EqualTo(0));
            Assert.That(result, Has.Property("Second").EqualTo(0));
        }
    }
}