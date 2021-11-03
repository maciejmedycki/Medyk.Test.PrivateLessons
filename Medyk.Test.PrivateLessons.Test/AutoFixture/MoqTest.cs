using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.AutoFixture
{
    [TestFixture]
    public class MoqTest
    {
        [Test]
        public void MoqHeavyWorkerSendData_RegularWay_LotsOfWriting()
        {
            var fixture = new Fixture();
            var loggerMock = new Mock<ILogger>();
            var apiMock = new Mock<IApiAccess>();
            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, null);

            sut.SendData(fixture.Create<int>(), fixture.Create<string>(), fixture.Create<object>());

            loggerMock.Verify(x=>x.Log(It.IsAny<string>()), Times.Exactly(3));//1ctor 2 method
            apiMock.Verify(x=>x.SendData(It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void HeavyWorkerWithAutoFixture_WontWork_ForAnInterface()
        {
            var fixture = new Fixture();
            var sut = fixture.Create<HeavyWorker>();//bang!
        }

        [Test]
        public void HeavyWorkerWithAutoMoq_ShouldCreateSut_JustFine()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            var sut = fixture.Create<HeavyWorker>();

            sut.SendData(fixture.Create<int>(), fixture.Create<string>(), fixture.Create<object>());

            //but how to check mocks from AutoMoq?
        }

        [Test]
        public void HeavyWorkerWithAutoMoq_TimeToUseFreeze()//https://blog.ploeh.dk/2010/03/17/AutoFixtureFreeze/
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            var apiMock = fixture.Freeze<Mock<IApiAccess>>();
            var sut = fixture.Create<HeavyWorker>();

            sut.SendData(fixture.Create<int>(), fixture.Create<string>(), fixture.Create<object>());

            apiMock.Verify(x => x.SendData(It.IsAny<object>()), Times.Once);
        }

        //Add parameter to HeavyWorker ctor
        //RhinoMock works similar as Moq. AutoRhinoMockCustomization. But works the other way round: create the stub and use AutoFixtures Inject(rhinoStub). No Freeze() call

        //Custom attribute that extends AutoData
        [Test]
        [AutoDataWithMoq]
        public void HeavyWorkerWithAutoMoqAutoDataAttribute(Mock<IApiAccess> apiMock, HeavyWorker sut, int id, string name, object payload)
        {
            sut.SendData(id, name, payload);

            apiMock.Verify(x => x.SendData(It.IsAny<object>()), Times.Once);//won't work - we need to freeze the apiMock 
        }

        [Test]
        [AutoDataWithMoq]
        public void HeavyWorkerWithAutoMoqAutoDataAttribute_FrozenAttributeInParam([Frozen] Mock<IApiAccess> apiMock, HeavyWorker sut, int id, string name, object payload)
        {
            sut.SendData(id, name, payload);

            apiMock.Verify(x => x.SendData(It.IsAny<object>()), Times.Once);//won't work - we need to freeze the apiMock 
        }
    }


    public class AutoDataWithMoqAttribute : AutoDataAttribute
    {
        public AutoDataWithMoqAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}