using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medyk.Test.PrivateLessons.Test
{
    [TestClass]
    public class MathTest
    {
        [TestMethod]
        public void DoMath_SmallPositiveInts_ReturnsGoodValue()
        {
            //Assign
            var sut = new Math();
            //Act
            var result = sut.DoMath(2, 2, 2);
            //Assert
            Assert.AreEqual(2, result);
        }
    }
}