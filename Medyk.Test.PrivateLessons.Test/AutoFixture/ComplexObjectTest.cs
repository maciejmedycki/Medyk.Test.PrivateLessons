using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Medyk.Test.PrivateLessons.AutoFixture.ComplexObject;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class ComplexObjectTest
    {
        [Test]
        public void Company_Arrange_IsKindaLong()
        {
            var owner = new Owner { Name = "BillyG", IsCompany = true, LastChange = DateTime.Now, Notes = new List<string> { "note1", "note2" } };
            var company = new Company() { IsGreat = true, Name = "myCompany", Owners = new List<Owner> { owner }, Established = DateTime.Now };

            //with autoFixture
            var fixture = new Fixture();
            var companyAnonymous = fixture.Create<Company>();
            //what's inside?
            //check Owner.Name length - DataAnnotations
        }

        [Test]
        public void TreeItem_Arrange_IsEvenLonger()
        {
            var children = new List<TreeItem>();
            Owner owner = new Owner { Name = "GG&MG" };
            Company company = new() { Name = "Systell", IsGreat = true, Owners = new List<Owner> { owner } };
            var treeItem = new TreeItem
            {
                Children = children,
                Company = company,
                Name = "root"
            };

            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
             .ToList()
             .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var treeItemAnonymous = fixture.Create<TreeItem>();
            //what's inside?
            //more here: https://stackoverflow.com/questions/31855842/how-do-i-customize-autofixture-behaviours-for-specific-classes
        }
    }
}