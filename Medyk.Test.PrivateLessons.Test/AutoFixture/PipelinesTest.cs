using System;
using System.Reflection;
using System.Text;
using AutoFixture;
using AutoFixture.Kernel;
using Medyk.Test.PrivateLessons.AutoFixture;
using Medyk.Test.PrivateLessons.AutoFixture.ComplexObject;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class PipelinesTest
    {
        //Pipeline: customization(ISpecimenBuilder)-> default builder -> residue collectors

        [Test]
        public void Person_PeselNotValid_Customize()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(new PeselSpecimenBuilder());

            var person = fixture.Create<Person>();
            var company = fixture.Create<Company>();
        }
    }
    public class PeselSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var propertyInfo = request as PropertyInfo;
            if (propertyInfo == null)
                return new NoSpecimen();
            
            var isPeselProperty = propertyInfo.Name == "Pesel" && propertyInfo.PropertyType == typeof(string);
            //we can add Contains("Name") and ignore casing - ever prop with name will have random pesel 
            //var isPeselProperty = (propertyInfo.Name == "Pesel" || propertyInfo.Name.Contains("name", StringComparison.InvariantCultureIgnoreCase)) && propertyInfo.PropertyType == typeof(string);
            if (isPeselProperty)
                return RandomPesel();
            return new NoSpecimen();
        }

        private string RandomPesel()
        {
            var rnd = new Random();
            var sb = new StringBuilder();
            for (var i = 0; i < 11; i++)
            {
                sb.Append(rnd.Next(0, 10));
            }
            return sb.ToString();
        }
    }

}