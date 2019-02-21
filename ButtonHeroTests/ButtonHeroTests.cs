using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ButtonHeroTests
{
    [TestClass]
    public class ButtonHeroTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Note note = new Note { Position = 4, Length = 1, Score = 1, Speed = 1 };
            Assert.IsTrue(note.Time1 == 4);
            Assert.IsTrue(note.Time2 == 5);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Note note = new Note { Position = 4, Length = 2, Score = 1, Speed = 2 };
            Assert.IsTrue(note.Time1 == 2);
            Assert.IsTrue(note.Time2 == 3);
        }

        [TestMethod]
        public void TestMethod3()
        {
            LinkedList<Note> list = new LinkedList<Note>();
            for (int i = 0; i < 100; i++)
            {
                Note note = new Note { Position = 4, Length = 2, Score = 1, Speed = 2 };
                list.AddLast(note);
                note = new Note { Position = 8, Length = 2, Score = 2, Speed = 2 };
                list.AddLast(note);
            }
            var result = NoteProblem.MergeEquivalents(list.ToArray());
            Assert.IsTrue(result.Length == 2);
            CollectionAssert.AreEqual(result.Select(e => e.Score).ToArray(), new int[] { 100, 200 });
        }

        [TestMethod]
        public void TestMethod4()
        {
            //4 2 1 1
            // 4 2 2 2

            LinkedList<Note> list = new LinkedList<Note>();
            Note note = new Note { Position = 4, Length = 2, Speed = 1, Score = 1 };
            list.AddLast(note);

            note = new Note { Position = 4, Length = 2, Speed = 2, Score = 2 };
            list.AddLast(note);

            var result = NoteProblem.Solve(list.ToArray());
            Assert.IsTrue(result == 3);
        }

        [TestMethod]
        public void TestMethod5()
        {
            //2 2 2 1
            //1 1 1 1

            LinkedList<Note> list = new LinkedList<Note>();
            Note note = new Note { Position = 2, Length = 2, Speed = 2, Score = 1 };
            list.AddLast(note);

            note = new Note { Position = 1, Length = 1, Speed = 1, Score = 1 };
            list.AddLast(note);

            var result = NoteProblem.Solve(list.ToArray());
            Assert.IsTrue(result == 2);
        }

        [TestMethod]
        public void TestMethod6()
        {
            //2 1 1 1
            //3 1 1 2
            //2 2 1 1

            LinkedList<Note> list = new LinkedList<Note>();
            Note note = new Note { Position = 2, Length = 1, Speed = 1, Score = 1 };
            list.AddLast(note);

            note = new Note { Position = 3, Length = 1, Speed = 1, Score = 2 };
            list.AddLast(note);

            note = new Note { Position = 2, Length = 2, Speed = 1, Score = 1 };
            list.AddLast(note);

            var result = NoteProblem.Solve(list.ToArray());
            Assert.IsTrue(result == 2);
        }

        [TestMethod]
        public void TestMethod7()
        {
            //18 2 2 5
            //1 2 1 1
            //16 10 2 3
            //8 10 2 4
            //27 6 3 5

            LinkedList<Note> list = new LinkedList<Note>();
            Note note = new Note { Position = 18, Length = 2, Speed = 2, Score = 5 };
            list.AddLast(note);

            note = new Note { Position = 1, Length = 2, Speed = 1, Score = 1 };
            list.AddLast(note);

            note = new Note { Position = 16, Length = 10, Speed = 2, Score = 3 };
            list.AddLast(note);

            note = new Note { Position = 8, Length = 10, Speed = 2, Score = 4 };
            list.AddLast(note);

            note = new Note { Position = 27, Length = 6, Speed = 3, Score = 5 };
            list.AddLast(note);

            var result = NoteProblem.Solve(list.ToArray());
            Assert.IsTrue(result == 6);
        }
    }
}
