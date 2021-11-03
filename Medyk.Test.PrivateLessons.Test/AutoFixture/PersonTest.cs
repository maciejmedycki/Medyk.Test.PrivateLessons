using System;
using System.Collections.Generic;
using AutoFixture;
using Medyk.Test.PrivateLessons.AutoFixture;
using Medyk.Test.PrivateLessons.AutoFixture.ComplexObject;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class PersonTest
    {
        [Test]
        public void Ctor_PeselValidation_FixtureThrowsException()
        {
            var fixture = new Fixture();
            //fixture.Inject("85061811111");

            var person = fixture.Create<Person>();
            //var myString = fixture.Create<string>();
        }

        [Test]
        public void Company_InjectObject_WorksAsExpected()
        {
            var fixture = new Fixture();
            fixture.Inject(new Owner { Name = "Medyk", LastChange = DateTime.Now });

            var company = fixture.Create<Company>();
        }

        [Test]
        public void Person_InjectObjectBuildWitAndWithout_MoreFancy()
        {
            var fixture = new Fixture();

            var person = fixture.Build<Person>()
                                .Without(x=>x.Pesel)
                                .With(x=>x.Surname, "FancySurnam")
                                .Create();

        }

        [Test]
        public void Person_OmitAllAutoProps_OmitsProp()
        {
            var fixture = new Fixture();

            var person = fixture.Build<Person>()
                .OmitAutoProperties().Create();                            
        }

        [Test]
        public void Company_WithoutAndDo_MorePower()
        {
            var fixture = new Fixture();

            var company = fixture.Build<Company>()
                                .Without(x => x.Owners)
                                .Do(x => x.Owners = new List<Owner>())
                                .Do(x => x.Owners.Add(fixture.Build<Owner>().With(x => x.Name, "owner 1").Create()))
                                .Do(x => x.Owners.Add(fixture.Build<Owner>().With(x => x.Name, "owner 2").Create()))
                                .Create();
        }
    }
}