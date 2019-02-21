using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Number = System.Int64;


namespace DoorscurityTests
{
    [TestClass]
    public class DoorscurityTests
    {

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(Utils.GCD(22, 10) == 2);
            Assert.IsTrue(Utils.GCD(-22, 10) == 2);
            Assert.IsTrue(Utils.GCD(105, 21) == 21);
            Assert.IsTrue(Utils.GCD(-105, 12) == 3);
            Assert.IsTrue(Utils.GCD(10, 22) == 2);
            Assert.IsTrue(Utils.GCD(33, -105) == 3);
        }

        [TestMethod]
        public void TestMethod2()
        {
            LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();
            Assert.IsTrue(Utils.GCD_Aux(22, 10, result) == 2);
            Assert.IsTrue(result.First.Value.Dividend == 22);
            Assert.IsTrue(result.First.Value.Divisor == 10);
            Assert.IsTrue(result.First.Value.Cocient == 2);
            Assert.IsTrue(result.First.Value.Rest == 2);
        }

        [TestMethod]
        public void TestMethod3()
        {
            LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();
            Utils.GCD_Aux(141, 34, result);
            Utils.GetEuclideanLinearEquation(result, out Number a1, out Number a2);
            Assert.IsTrue(a1 == 7 && a2 == -29);
        }

        [TestMethod]
        public void TestMethod4()
        {
            LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();
            Utils.GCD_Aux(9, 8, result);
            Utils.GetEuclideanLinearEquation(result, out Number a1, out Number a2);
            Assert.IsTrue(a1 == 1 && a2 == -1);
        }

        [TestMethod]
        public void TestMethod5()
        {
            LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();
            Utils.GCD_Aux(19, 7, result);
            Utils.GetEuclideanLinearEquation(result, out Number a1, out Number a2);
            Assert.IsTrue(a1 == 3 && a2 == -8);
        }

        [TestMethod]
        public void TestMethod6()
        {
            LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();
            Utils.SolveDiophanLinearEquation(19, 7, 20 , out Door door1, out Door door2);
            Assert.IsTrue(-door1.Independent * 19 + -door2.Independent * 7 == 20);            
        }

        [TestMethod]
        public void TestMethod7()
        {
            LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();
            Utils.SolveDiophanLinearEquation(-19, 7, 20, out Door door1, out Door door2);
            var resultequation = -door1.Independent * (-19) + -door2.Independent * 7;
            Assert.IsTrue(resultequation == 20);

            Number firstNumber = 7;
            Number secondNumber = -19;
            Number thirdNumber = 20;
            Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out door1, out door2);
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);

            firstNumber = -7;
            secondNumber = -19;
            thirdNumber = 20;

            Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out door1, out door2);
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);

            firstNumber = -19;
            secondNumber = -7;
            thirdNumber = 20;

            Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out door1, out door2);
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);

            firstNumber = -6;
            secondNumber = -4;
            thirdNumber = 20;

            Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out door1, out door2);
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);
        }

        [TestMethod]
        public void TestMethod8()
        {
            Door[] doors = new Door[]
            {
                new Door{ Period = 2 , TimePassed = 1 }
                , new Door{ Period = 3 , TimePassed = 1 }
                , new Door{ Period = 4 , TimePassed = 1 }
                , new Door{ Period = 6 , TimePassed = 2 }
            };
            Doorscurity doorscurity = new Doorscurity(doors);
            Assert.IsTrue(doorscurity.Solve(out Number result));
            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void TestMethod9()
        {
            Door[] doors = new Door[]
            {
                new Door{ Period = 2 , TimePassed = 1 }
                , new Door{ Period = 3 , TimePassed = 1 }
                , new Door{ Period = 4 , TimePassed = 1 }
                , new Door{ Period = 4 , TimePassed = 1 }
            };
            Doorscurity doorscurity = new Doorscurity(doors);
            Assert.IsFalse(doorscurity.Solve(out Number result));
        }

        [TestMethod]
        public void TestMethod10()
        {
            Door[] doors = new Door[]
            {
                new Door{ Period = 2 , TimePassed = 0 }                
            };
            Doorscurity doorscurity = new Doorscurity(doors);
            Assert.IsTrue(doorscurity.Solve(out Number result));
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void TestMethod11()
        {

            Door[] doors = new Door[]
            {
                new Door{ Period = 7 , TimePassed = 2 }
                ,new Door{ Period = 9 , TimePassed = 3 }
            };
            Doorscurity doorscurity = new Doorscurity(doors);
            Assert.IsTrue(doorscurity.Solve(out Number result));
            Assert.IsTrue(result == 5);
        }

        

        [TestMethod]
        public void TestMethod12()
        {

            Door[] doors = new Door[]
            {
                new Door{ Period = 5 , TimePassed = 2 }
                ,new Door{ Period = 9 , TimePassed = 3 }
                ,new Door{ Period = 7 , TimePassed = 2 }
            };
            Doorscurity doorscurity = new Doorscurity(doors);
            Assert.IsTrue(doorscurity.Solve(out Number result));
            Assert.IsTrue(result == 248);
        }

        

        [TestMethod]
        public void TestMethod13()
        {

            Door[] doors = new Door[]
            {
                new Door{ Period = 7 , TimePassed = 2 }
                ,new Door{ Period = 23 , TimePassed = 20 }
                ,new Door{ Period = 21 , TimePassed = 20 }
                ,new Door{ Period = 27 , TimePassed = 3 }
            };
            Doorscurity doorscurity = new Doorscurity(doors);
            Assert.IsFalse(doorscurity.Solve(out Number result));
        }

        [TestMethod]
        public void TestMethod14()
        {

            Door[] doors = new Door[]
            {
                new Door{ Period = 59 , TimePassed = 23 }
                ,new Door{ Period = 65 , TimePassed = 25 }
                ,new Door{ Period = 34 , TimePassed = 15 }
                ,new Door{ Period = 9 , TimePassed = 3 }
            };
            Doorscurity doorscurity = new Doorscurity(doors);
            Assert.IsTrue(doorscurity.Solve(out Number result));
            Assert.IsTrue(result == 711399);
        }

        [TestMethod]
        public void TestMethod15()
        {
            LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();
            var resultGCD = Utils.GCD(-130390,9, result);
            Assert.IsTrue(resultGCD == 1);

            resultGCD = Utils.GCD(9,-130390,  result);
            Assert.IsTrue(resultGCD == 1);

            resultGCD = Utils.GCD(-9, -130390, result);
            Assert.IsTrue(resultGCD == 1);
        }

        [TestMethod]
        public void TestMethod16()
        {
            Number firstNumber = -130390;
            Number secondNumber = 9;
            Number thirdNumber = -36971314;
            Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out Door door1, out Door door2);
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);

            for (int c = 0; c < 50; c++)
            {
                Assert.IsTrue((-door1.Independent + door1.Period*c) * (firstNumber) + ( -door2.Independent + door2.Period * c) * (secondNumber) == thirdNumber);
            }
        }

        [TestMethod]
        public void TestMethod18()
        {
            Number firstNumber = -199;
            Number secondNumber = 658;
            Number thirdNumber = -438;
            Assert.IsTrue(Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out Door door1, out Door door2));
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);

            for (int c = 0; c < 50; c++)
            {
                Assert.IsTrue((-door1.Independent + door1.Period * c) * (firstNumber) + (-door2.Independent + door2.Period * c) * (secondNumber) == thirdNumber);
            }
        }

        [TestMethod]
        public void TestMethod19()
        {
            Number firstNumber = -1;
            Number secondNumber = 285;
            Number thirdNumber = -229;
            Assert.IsTrue(Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out Door door1, out Door door2));
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);

            for (int c = 0; c < 50; c++)
            {
                Assert.IsTrue((-door1.Independent + door1.Period * c) * (firstNumber) + (-door2.Independent + door2.Period * c) * (secondNumber) == thirdNumber);
            }
        }

        [TestMethod]
        public void TestMethod20()
        {
            Number firstNumber = 1;
            Number secondNumber = 1;
            Number thirdNumber = 1;
            Assert.IsTrue(Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out Door door1, out Door door2));
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);

            for (int c = 0; c < 50; c++)
            {
                Assert.IsTrue((-door1.Independent + door1.Period * c) * (firstNumber) + (-door2.Independent + door2.Period * c) * (secondNumber) == thirdNumber);
            }
        }

        [TestMethod]
        public void TestMethod21()
        {
            Number firstNumber = 2;
            Number secondNumber = 2;
            Number thirdNumber = 2;
            Assert.IsTrue(Utils.SolveDiophanLinearEquation(firstNumber, secondNumber, thirdNumber, out Door door1, out Door door2));
            Assert.IsTrue(-door1.Independent * (firstNumber) + -door2.Independent * (secondNumber) == thirdNumber);

            for (int c = 0; c < 50; c++)
            {
                Assert.IsTrue((-door1.Independent + door1.Period * c) * (firstNumber) + (-door2.Independent + door2.Period * c) * (secondNumber) == thirdNumber);
            }
        }

        [TestMethod]
        public void TestMethod22()
        {
            LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();
            Utils.GCD(141, 34, result);
            Utils.GetEuclideanLinearEquation(result, out Number a1, out Number a2);
            Assert.IsTrue(a1 == 7 && a2 == -29);

            result = new LinkedList<EuclideanResult>();
            Number n1 = 1;
            Number n2 = 1;
            Utils.GCD(n1,n2, result);
            Utils.GetEuclideanLinearEquation(result, out  a1, out  a2);
            Assert.IsTrue(n1 * a1 + n2 * a2 == 1);


            result = new LinkedList<EuclideanResult>();
            n1 = 33;
            n2 = 20;            
            var gcd = Utils.GCD(n1, n2, result);
            Utils.GetEuclideanLinearEquation(result, out a1, out a2);
            Assert.IsTrue(n1 * a1 + n2 * a2 == 1);


        }
    }
}
