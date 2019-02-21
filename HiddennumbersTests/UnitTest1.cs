using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HiddennumbersTests
{
    [TestClass]
    public class InfiniteNumberTest
    {
        [TestMethod]
        public void TestMethod0plus1equals1_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("0".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("1".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 + n2;
            Assert.AreEqual(n3.ToString() , "1");
        }

        [TestMethod]
        public void TestMethod10234plus42310_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("10234".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("42310".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 + n2;
            Assert.AreEqual(n3.ToString(), (10234+42310).ToString());
        }

        [TestMethod]
        public void TestMethod9999plus1_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("9999".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("1".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 + n2;
            Assert.AreEqual(n3.ToString(), (9999 + 1).ToString());
        }

        [TestMethod]
        public void TestMethod1Jplus1_Base20()
        {
            InfiniteNumber n1 = new InfiniteNumber("1J".Select(c => c.ToString()).ToArray(), 20);
            InfiniteNumber n2 = new InfiniteNumber("1".Select(c => c.ToString()).ToArray(), 20);
            var n3 = n1 + n2;
            Assert.AreEqual(n3.ToString(), (20).ToString());
        }

        [TestMethod]
        public void TestMethod9diff1_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("9".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("1".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 - n2;
            Assert.AreEqual(n3.ToString(), (8).ToString());
        }

        [TestMethod]
        public void TestMethod10000diff1_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("10000".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("1".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 - n2;
            Assert.AreEqual(n3.ToString(), (9999).ToString());
        }

        [TestMethod]
        public void TestMethod987diff90_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("987".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("90".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 - n2;
            Assert.AreEqual(n3.ToString(), (897).ToString());
        }

        [TestMethod]
        public void TestMethod10000diff1_Base20()
        {
            InfiniteNumber n1 = new InfiniteNumber("10000".Select(c => c.ToString()).ToArray(), 20);
            InfiniteNumber n2 = new InfiniteNumber("1".Select(c => c.ToString()).ToArray(), 20);
            var n3 = n1 - n2;
            Assert.AreEqual(n3.ToString(), "JJJJ");
        }

        [TestMethod]
        public void TestMethod10000diff10000_Base20()
        {
            InfiniteNumber n1 = new InfiniteNumber("10000".Select(c => c.ToString()).ToArray(), 20);
            InfiniteNumber n2 = new InfiniteNumber("10000".Select(c => c.ToString()).ToArray(), 20);
            var n3 = n1 - n2;
            Assert.AreEqual(n3.ToString(), "0");
        }

        [TestMethod]
        public void TestMethod10000prod10000_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("10000".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("10000".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 * n2;
            Assert.AreEqual(n3.ToString(), "100000000");
        }


        [TestMethod]
        public void TestMethod10pow40prod26_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("26".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 * n2;
            Assert.AreEqual(n3.ToString(), "26000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
        }

        [TestMethod]
        public void TestMethod10pow40prod0_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("0".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 * n2;
            Assert.AreEqual(n3.ToString(), "0");
        }
        [TestMethod]
        public void TestMethod1234567890prod1_Base10()
        {
            InfiniteNumber n1 = new InfiniteNumber("1234567890".Select(c => c.ToString()).ToArray(), 10);
            InfiniteNumber n2 = new InfiniteNumber("1".Select(c => c.ToString()).ToArray(), 10);
            var n3 = n1 * n2;
            Assert.AreEqual(n3.ToString(), "1234567890");
        }

        [TestMethod]
        public void TestMethod100base3ToBase10()
        {
            InfiniteNumber n1 = new InfiniteNumber("10".Select(c => c.ToString()).ToArray(), 3);
            var n3 = n1.ToBase10();
            Assert.AreEqual(n3.ToString() , "3");
        }

        [TestMethod]
        public void TestMethodManybasesToBase10()
        {
            for (int baseN = 2; baseN < 27; baseN++)
            {
                InfiniteNumber n1 = new InfiniteNumber("10".Select(c => c.ToString()).ToArray(), baseN);
                var n3 = n1.ToBase10();
                Assert.AreEqual(n3.ToString(), baseN.ToString());
            }
            
        }
    }
}
