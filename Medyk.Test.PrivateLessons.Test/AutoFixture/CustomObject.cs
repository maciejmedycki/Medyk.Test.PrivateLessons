using AutoFixture;
using Medyk.Test.PrivateLessons.AutoFixture.CustomObject;
using Moq;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class CustomObject
    {
        [Test]
        public void DataHandlerAdd_AddingManualTestData_AddsData()
        {
            DataDTO dataDto = new(1, "medyk");
            dataDto.Title = "unit test";
            var sut = new DataHandler();

            sut.Add(dataDto);

            Assert.That(sut.Data.Count, Is.EqualTo(1));//I don't care about details, just if it was added
        }


        [Test]
        public void DataHandlerAdd_AddingAnonymousTestData_AddsData()
        {
            var fixture = new Fixture();            
            var sut = new DataHandler();

            sut.Add(fixture.Create<DataDTO>());

            Assert.That(sut.Data.Count, Is.EqualTo(1));//I don't care about details, just if it was added
            //let's see what's inside
            var checkMe = sut.Data[0];
        }

        [Test]
        public void DataHandlerAdd_CanMoqHandleIt_Maybe()        
        {
            var mock = new Mock<DataDTO>();
            //var mock = new Mock<DataDTO>(1, "myMock");
            var sut = new DataHandler(); 
            
            sut.Add(mock.Object);

            Assert.That(sut.Data.Count, Is.EqualTo(1));//I don't care about details, just if it was added
            //let's see what's inside
            var checkMe = sut.Data[0];
        }

    }
}