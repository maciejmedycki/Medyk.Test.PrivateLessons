using System;
using Moq;
using NUnit.Framework;
using Moq.Protected;

namespace Medyk.Test.PrivateLessons.Test.Moq
{
    [TestFixture]
    public class MoqTest
    {
        [Test]
        public void HeavyWorker_Ctor_CallsLogger()
        {
            var apiMock = new Mock<IApiAccess>();
            var loggerMock = new Mock<ILogger>();

            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, null);

            loggerMock.Verify(x => x.Log("HeavyWorker created"));//risky while refactoring, use nameof maybe
        }

        [Test]
        public void HeavyWorker_Ctor_CallsLoggerIsAny()
        {
            var apiMock = new Mock<IApiAccess>();
            var loggerMock = new Mock<ILogger>();

            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, null);

            loggerMock.Verify(x => x.Log(It.IsAny<string>()));
        }

        [Test]
        public void HeavyWorker_Ctor_CallsLoggerIsSpecific()
        {
            var apiMock = new Mock<IApiAccess>();
            var loggerMock = new Mock<ILogger>();

            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, null);

            loggerMock.Verify(x => x.Log(It.Is<string>(x => x.Contains("created") && x.Contains(nameof(HeavyWorker)))));
        }

        [Test]
        public void HeavyWorker_SendData_CallLoggerAndApi()
        {
            var apiMock = new Mock<IApiAccess>();
            var loggerMock = new Mock<ILogger>();
            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, null);
            var id = 123;
            var name = "myName";
            var payload = ("asf", 123, "123123123");

            sut.SendData(id, name, payload);


            loggerMock.Verify(x => x.Log(It.IsAny<string>()));
            apiMock.Verify(x=>x.SendData(It.IsAny<object>()));
        }

        //setup, verifiable
        [Test]
        public void HeavyWorker_ValidateAndSendData_SetupMock()
        {
            var apiMock = new Mock<IApiAccess>();
            var loggerMock = new Mock<ILogger>();
            var validatorMock = new Mock<IDataValidator>();            
            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, validatorMock.Object);
            var id = 123;
            var name = "myName";
            var payload = ("asf", 123, "123123123");
            validatorMock.Setup(x => x.IsValid(payload)).Returns(true).Verifiable();

            sut.ValidateAndSendData(id, name, payload);

            validatorMock.Verify();
            apiMock.Verify(x => x.SendData(It.IsAny<object>()));
        }

        [Test]
        public void INeedTo_ReturnNull_ForReferenceType_Fails()
        {
            var validatorMock = new Mock<IDataValidator>();

            //validatorMock.Setup(x => x.GetValidationMessage(It.IsAny<object>())).Returns(null);//nope!
            validatorMock.Setup(x => x.GetValidationMessage(It.IsAny<object>())).Returns<string>(null);

            //validatorMock.Setup(x => x.IsValid(It.IsAny<object>())).Returns(null);//nope!
            validatorMock.Setup(x => x.IsValid(It.IsAny<object>())).Returns<bool>(null);//nope!


            Assert.IsNull(validatorMock.Object.GetValidationMessage(new object()));
            Assert.IsNull(validatorMock.Object.IsValid(new object()));
        }


        [Test]
        public void SetupProperty_IsAsEasy_AsSetupMethod()
        {
            var apiMock = new Mock<IApiAccess>();
            var loggerMock = new Mock<ILogger>();
            var validatorMock = new Mock<IDataValidator>();
            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, validatorMock.Object);
            var id = 123;
            var name = "myName";
            var payload = ("asf", 123, "123123123");
            validatorMock.Setup(x=>x.Disabled).Returns(true).Verifiable();            

            sut.ValidateAndSendData(id, name, payload);

            validatorMock.Verify();
            validatorMock.Verify(x => x.IsValid(payload), Times.Never);
            apiMock.Verify(x => x.SendData(It.IsAny<object>()), Times.Never);
        }


        [Test]
        public void MoqCanTrackChangesOfProperty_SetupProperty_DoesItsMagic()
        {
            var apiMock = new Mock<IApiAccess>();
            var loggerMock = new Mock<ILogger>();
            var validatorMock = new Mock<IDataValidator>();
            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, validatorMock.Object);
            var counterMock = new Mock<ICounter>();
            //counterMock.Setup(x => x.Count);
            counterMock.SetupProperty(x => x.Count);
            //counterMock.SetupProperty(x => x.Count, 10);
            //counterMock.SetupAllProperties();//just do all the props for me please! But call it after all your props setup!
            sut.Counter = counterMock.Object; 
            var id = 123;
            var name = "myName";
            var payload = ("asf", 123, "123123123");
           
            sut.SendData(id, name, payload);

            Assert.That(sut.Counter.Count, Is.EqualTo(1));
            counterMock.VerifyGet(x => x.Count);//VerifyGet()
            counterMock.VerifySet(x => x.Count = 1);//VerifySet()
            counterMock.VerifyNoOtherCalls();//IDoNothing was not called
        }

        [Test]
        public void Moq_CanBe_Strict_Fails()
        {
            var looseMock = new Mock<ICounter>();
            looseMock.Object.Count++;

            var strictMock = new Mock<ICounter>(MockBehavior.Strict);
            strictMock.Object.Count++;
        }

        [Test]
        public void Moq_CanThrowErrors_Yay()
        {
            var apiMock = new Mock<IApiAccess>();
            var message = "I don't like it!";
            apiMock.Setup(x=>x.SendData(It.IsAny<object>())).Throws(new ArgumentException(message));
            var loggerMock = new Mock<ILogger>();
            var validatorMock = new Mock<IDataValidator>();
            var sut = new HeavyWorker(apiMock.Object, loggerMock.Object, validatorMock.Object);
            var id = 123;
            var name = "myName";
            var payload = ("asf", 123, "123123123");

            sut.SendData(id, name, payload);

            loggerMock.Verify(x => x.Log(message));
        }

        [Test]
        public void PartialMock_WeCanMockClasses_NotInterfaces()
        {
            var loggerMock = new Mock<Logger>();
            //loggerMock.Setup(x=>x.LogToFile(It.IsAny<string>()));//doesn't work for private and non virtual
            loggerMock.Setup(x=>x.LogToFile2(It.IsAny<string>())).Verifiable();

            var sut = new HeavyWorker(null, loggerMock.Object, null);

            loggerMock.Verify();
        }

        [Test]
        public void HowToTest_DateTimeNow_EasyWay()
        {
            var validatorMock = new Mock<Validator>();

            var result = validatorMock.Object.IsValid(new object());

            Assert.IsTrue(result);
            Assert.That(validatorMock.Object.LastCheck, Is.EqualTo(DateTime.Now).Within(5).Seconds);//meh...
        }

        [Test]
        public void HowToTest_DateTimeNow_RefactorWay()
        {
            var validatorMock = new Mock<ValidatorBetterOne>();
            var dt = DateTime.Now;
            validatorMock.Setup(x=>x.GetDateNow()).Returns(dt);
            var result = validatorMock.Object.IsValid(new object());

            Assert.IsTrue(result);
            Assert.That(validatorMock.Object.LastCheck, Is.EqualTo(dt));
        }

        [Test]
        public void OkOk_ButIDontWant_ToMakeItPublic()//using Moq.Protected
        {
            var validatorMock = new Mock<ValidatorBetterTwo>();
            var dt = DateTime.Now;
            validatorMock.Protected().Setup<DateTime>("GetDateNow").Returns(dt);//protected is the best one Moq can offer. Refactor of GetDateNow name is sadly painful
            var result = validatorMock.Object.IsValid(new object());

            Assert.IsTrue(result);
            Assert.That(validatorMock.Object.LastCheck, Is.EqualTo(dt));
        }

        //or you can maybe create another abstract Interface like INowProvider...

        [Test]
        public void Moq_Callback_CanBeUseful()
        {
            var validatorMock = new Mock<IDataValidator>();
            var payload = "myString";
            validatorMock.Setup(x=>x.IsValid(It.IsAny<object>())).Callback(()=> { payload = "callback time!"; });

            validatorMock.Object.IsValid(payload);

            Assert.That(payload, Is.EqualTo("callback time!"));
        }        
    }
}