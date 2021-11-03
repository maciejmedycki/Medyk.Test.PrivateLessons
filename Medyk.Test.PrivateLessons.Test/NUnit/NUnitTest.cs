using System;
using NUnit.Framework;

namespace Medyk.Test.PrivateLessons.Test.NUnit
{
    [TestFixture]
    public class NUnitTest
    {
        [Test]
        public void Add_Test_Case1()
        {
            var a = 12;
            var b = 8;

            var result = Add(a, b);

            Assert.AreEqual(result, 20);
        }

        [Test]
        public void Add_Test_Case2()
        {
            var a = -8;
            var b = -2;

            var result = Add(a, b);

            Assert.AreEqual(result, -10);
        }

        //and so on for more cases, but with NUnit:
        [Test]
        [TestCase(12, 8)]
        [TestCase(-8, -2)]
        public void Add_Test_CaseNUnit(int a, int b)
        {
            var result = Add(a, b);

            Assert.AreEqual(result, a + b);
        }

        [Test, Combinatorial]
        public void Add_Test_CaseNUnit_AllWithAll([Values(1, 2, 3)] int a, [Random(-100, 100, 5)] int b)
        {
            var result = Add(a, b);

            Assert.AreEqual(result, a + b);
        }

        [Test, Timeout(1000)]//in DEBUG the timeout is not enforced!
        public void TimeOutTest_LetCalculatePi_AreWeQuickEnough_Maybe()
        {
            double pi;
            var maxLoop = 10;//let's play with this value
            pi = 2 * F(1, maxLoop);

            Assert.That(pi, Is.AtLeast(3.13));
            Assert.That(pi, Is.AtMost(3.15));
            Console.WriteLine(pi);
        }


        [Test]
        public void WhatAboutExceptions_JustCatchThem_AndAssert()
        {
            Assert.Throws<ArgumentNullException>(() => { ThrowException("it fails on a purpose"); });
        }

        [Test]
        public void StringAsset_FancyStringCheck_CanBeFun()
        {
            var s1 = "aaa";
            var s2 = "bbb";
            
            (s1, s2) = (s2, s1);//what?

            StringAssert.AreEqualIgnoringCase("BBB", s1);
            StringAssert.AreEqualIgnoringCase("AAA", s2);
        }
        
        //more in https://www.automatetheplanet.com/nunit-cheat-sheet/

        private int Add(int a, int b)
        {
            return a + b;
        }

        private double F(int i, int maxLoop)
        {
            System.Threading.Thread.Sleep(20);
            if (i >= maxLoop)
                return 1;
            return 1 + i / (2.0 * i + 1) * F(i + 1, maxLoop);
        }

        private void ThrowException(string message)
        {
            throw new ArgumentNullException(message);
        }
    }
}