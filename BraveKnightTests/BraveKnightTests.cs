using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BraveKnightTests
{
    [TestClass]
    public class BraveKnightTests
    {
        [ClassInitialize]
        public static void Initializer(TestContext context)
        {

        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] terrain = new string[]
            {
                "P...S"
               ,"....."
               ,"....."
               ,"....."
               ,"....D"
            };
            BraveKnight braveKnight = new BraveKnight(terrain);
            Assert.IsTrue(braveKnight.Princess.Row == 0 && braveKnight.Princess.Column == 0);
            Assert.IsTrue(braveKnight.Start.Row == 0 && braveKnight.Start.Column == 4);
            Assert.IsTrue(braveKnight.Destination.Row == 4 && braveKnight.Destination.Column == 4);

            Assert.IsTrue(braveKnight.graph[0].Keys.Count == 2);
            Assert.IsTrue(braveKnight.graph[0].ContainsKey(7));
            Assert.IsTrue(braveKnight.graph[0].ContainsKey(11));

            Assert.IsTrue(braveKnight.graph[4].Keys.Count == 2);
            Assert.IsTrue(braveKnight.graph[4].ContainsKey(7));
            Assert.IsTrue(braveKnight.graph[4].ContainsKey(13));

            Assert.IsTrue(braveKnight.graph[12].Keys.Count == 8);
            Assert.IsTrue(braveKnight.graph[12].ContainsKey(9));
            Assert.IsTrue(braveKnight.graph[12].ContainsKey(19));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] terrain = new string[]
            {
                "P...S"
               ,"..#.."
               ,"....."
               ,"....."
               ,"....D"
            };
            BraveKnight braveKnight = new BraveKnight(terrain);
            Assert.IsTrue(braveKnight.Princess.Row == 0 && braveKnight.Princess.Column == 0);
            Assert.IsTrue(braveKnight.Start.Row == 0 && braveKnight.Start.Column == 4);
            Assert.IsTrue(braveKnight.Destination.Row == 4 && braveKnight.Destination.Column == 4);

            Assert.IsTrue(braveKnight.graph[0].Keys.Count == 1);
            Assert.IsTrue(braveKnight.graph[0].ContainsKey(11));

            Assert.IsTrue(braveKnight.graph[4].Keys.Count == 1);
            Assert.IsTrue(braveKnight.graph[4].ContainsKey(13));

            Assert.IsTrue(braveKnight.graph[12].Keys.Count == 8);
            Assert.IsTrue(braveKnight.graph[12].ContainsKey(9));
            Assert.IsTrue(braveKnight.graph[12].ContainsKey(19));
        }

        [TestMethod]
        public void TestMethod4()
        {
            string[] terrain = new string[]
            {
                "P...S"
               ,"..#.."
               ,"....."
               ,"....."
               ,"....D"
            };
            BraveKnight braveKnight = new BraveKnight(terrain);
            Assert.IsTrue(braveKnight.MinDistance(0, 4)==4);
        }

        [TestMethod]
        public void TestMethod5()
        {
            string[] terrain = new string[]
            {
                "P...S"
               ,"..#.."
               ,".#..."
               ,"....."
               ,"....D"
            };
            BraveKnight braveKnight = new BraveKnight(terrain);
            Assert.IsFalse(braveKnight.MinDistance(0, 24).HasValue );
        }

        [TestMethod]
        public void TestMethod6()
        {
            string[] terrain = new string[]
            {
                "P...S"
               ,"....."
               ,"....."
               ,"....."
               ,"....D"
            };
            BraveKnight braveKnight = new BraveKnight(terrain);
            braveKnight.ExistShortestRescue(out int distance);
            Assert.IsTrue(distance == 6);
        }

        [TestMethod]
        public void TestMethod7()
        {
            string[] terrain = new string[]
            {
                "P...S"
               ,"....."
               ,"....."
               ,"....."
               ,"..*.D"
            };
            BraveKnight braveKnight = new BraveKnight(terrain);
            Assert.IsTrue(braveKnight.MinDistance(22, 0) == 1);
        }

        [TestMethod]
        public void TestMethod8()
        {
            string[] terrain = new string[]
            {
                "P...S"
               ,"....."
               ,"....."
               ,"....."
               ,"..*.D"
            };
            BraveKnight braveKnight = new BraveKnight(terrain);
            Assert.IsTrue(braveKnight.MinDistance(24, 0) == 3);
        }

    }
}
