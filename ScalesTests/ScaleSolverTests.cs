using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScalesTests
{
    [TestClass]
    public class ScaleSolverTests
    {
        private static ScaleSolver scaleSolver;

        [ClassInitialize]
        public static void Initializer(TestContext context)
        {
            scaleSolver = new ScaleSolver();
        }
        [TestMethod]
        public void TestMethod1()
        {
            string[] notes = "C C G G A A G".Split(' ');
            var ssResult = scaleSolver.GetKeys(notes);
            string result = "";
            result += ssResult[0];
            for (int i = 1; i < ssResult.Length; i++)
            {
                result += " " + ssResult[i];
            }

            Assert.AreEqual(result, "MA# MC MF MG mA mD mE mG");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var result = scaleSolver.majorScales["MC"];
            CollectionAssert.AreEqual(result, new bool[] { true, false, true, false, true, true, false, true, false, true, false, true });

            result = scaleSolver.majorScales["MA"];
            CollectionAssert.AreEqual(result, new bool[] { false, true, true, false, true, false, true, false, true, true, false, true });
        }

        [TestMethod]
        public void TestMethod3()
        {
            string[] notes = "E D# E D# E B D C A".Split(' ');
            var ssResult = scaleSolver.GetKeys(notes);
            string result = "";
            //
            Assert.AreEqual(ssResult.Length, 0);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string[] notes = "E D# E F# G# G# F# G# A A G# C# B A G# D# E F# G# G# F# E".Split(' ');
            var ssResult = scaleSolver.GetKeys(notes);
            string result = "";
            result += ssResult[0];
            for (int i = 1; i < ssResult.Length; i++)
            {
                result += " " + ssResult[i];
            }

            Assert.AreEqual(result, "ME mC#");
        }

        [TestMethod]
        public void TestMethod5()
        {
            string[] notes = "E".Split(' ');
            var ssResult = scaleSolver.GetKeys(notes);
            string result = "";
            result += ssResult[0];
            for (int i = 1; i < ssResult.Length; i++)
            {
                result += " " + ssResult[i];
            }

            Assert.AreEqual(result, "MA MB MC MD ME MF MG mA mB mC# mD mE mF# mG#");
        }

        [TestMethod]
        public void TestMethod6()
        {
            string[] notes = "Fb".Split(' ');
            var ssResult = scaleSolver.GetKeys(notes);
            string result = "";
            result += ssResult[0];
            for (int i = 1; i < ssResult.Length; i++)
            {
                result += " " + ssResult[i];
            }

            Assert.AreEqual(result, "MA MB MC MD ME MF MG mA mB mC# mD mE mF# mG#");
        }
    }
}
