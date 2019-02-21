using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Number = System.Int64;

public class Solution
{
    /// <summary>
    /// Explanation :      
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        try
        {
            int C = int.Parse(Console.ReadLine());
            for (int i = 1; i <= C; i++)
            { 
                try
                {
                    int D = int.Parse(Console.ReadLine());
                    LinkedList<Door> doors = new LinkedList<Door>();
                    for (int j = 0; j < D ; j++)
                    {
                        string[] stringParts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int P = int.Parse(stringParts[0]);
                        int T = int.Parse(stringParts[1]);
                        doors.AddLast(new Door { Period = P, TimePassed = T});
                    }
                    Doorscurity doorscurity = new Doorscurity(doors.ToArray());
                    var doorsCopy = doors.Select(d => new Door { Independent = d.Independent, Index = d.Index, Period = d.Period, TimePassed = d.TimePassed }).ToArray();
                    string prefixString = "Case #" + i + ": ";

                    if (doorscurity.Solve(out Number result))
                    {
                        Console.WriteLine(prefixString + result);
                        if (!Doorscurity.VerifyFeasible(doorsCopy, result))
                            Console.WriteLine("BAAAAD ANSWER");

                    }
                    else
                        Console.WriteLine(prefixString + "NEVER");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Case #" + i + ": ");
                    Console.WriteLine(exception);
                }
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}

public class Doorscurity
{
    public Doorscurity(Door[] doors)
    {
        this.Doors = doors;
        for (Number i = 0; i < doors.Length; i++)
        {
            doors[i].Index = i;
        }

        foreach (var door in Doors)
        {
            if (door.TimePassed == 0)
                door.TimePassed = door.Period;

            door.Independent = -((door.Period - door.TimePassed)) + door.Index;
            //door.Independent = -door.Period + door.TimePassed + door.Index;
        }
    }

    public Door[] Doors { get; private set; }

    public bool Solve(out Number result)
    {
        result = 0;

        if (Doors.Count() == 1)
        {
            result = -Doors.First().Independent;
            return true;
        }


        /////////////////////////

        Door ValueDoor = Doors[0];  
        if (!Utils.SolveDiophanLinearEquation(-ValueDoor.Period, Doors[1].Period, -ValueDoor.Independent + Doors[1].Independent, out Door door1, out Door door2))
            return false;
        LinkedList<Door> doorsConstraints = new LinkedList<Door>();

        ValueDoor.Independent = ValueDoor.Independent + ValueDoor.Period * door1.Independent;
        ValueDoor.Period = ValueDoor.Period * door1.Period;        

        doorsConstraints.AddLast(door1);
        doorsConstraints.AddLast(door2);

        /////////////////////////


        foreach (var door in Doors.Skip(2))
        {
            if (!Utils.SolveDiophanLinearEquation(-ValueDoor.Period, door.Period, -ValueDoor.Independent + door.Independent, out  door1, out  door2))
                return false;

            ValueDoor.Independent = ValueDoor.Independent + ValueDoor.Period * door1.Independent;
            ValueDoor.Period = ValueDoor.Period * door1.Period;

            foreach (var doorscontraint in doorsConstraints)
            {
                doorscontraint.Independent = doorscontraint.Independent + doorscontraint.Period * door1.Independent;
                doorscontraint.Period = doorscontraint.Period * door1.Period;
            }

            doorsConstraints.AddLast(door2);
        }

        doorsConstraints.AddLast(ValueDoor);
        if (!CreateInterval(doorsConstraints, out Number minIntValue, out Number maxIntValue))
            return false;

        if (ValueDoor.Period == 0)
        {
            if ((-ValueDoor.Independent) <= maxIntValue && (-ValueDoor.Independent) >= minIntValue)
            {
                result = ValueDoor.Independent;
            }
            else
                return false;
        }
        else if (  ValueDoor.Period < 0 )  
        {
            var value = ValueDoor.Period*maxIntValue -ValueDoor.Independent;
            result = value;
        }
        else //  if (ValueDoor.Period < 0)
        {
            var value = ValueDoor.Period * minIntValue - ValueDoor.Independent;
            result = value;
        }

        return true;
    }

    private bool CreateInterval(LinkedList<Door> doorsConstraints, out Number minIntValue, out Number maxIntValue)
    {
        double min = double.NegativeInfinity;
        double max = double.PositiveInfinity;
        minIntValue = 0;
        maxIntValue = 0;

        foreach (var doorsConstraint in doorsConstraints)
        {
            bool equalGT = doorsConstraint.Period >= 0;
            if ( equalGT )
            {
                min =Math.Max(min, Math.Ceiling(((double)doorsConstraint.Independent) / doorsConstraint.Period));
            }
            else
            {
                max = Math.Min(max, Math.Floor(((double)doorsConstraint.Independent) / doorsConstraint.Period));
            }
            if (min > max)
                return false;
        }

        minIntValue = (Number)min;
        maxIntValue = (Number)max;

        return true;
    }

    public static bool VerifyFeasible(Door[] doors, Number vResult )
    {
        for (int i = 0; i < doors.Length ; i++)
        {
            if (!((vResult + doors[i].Independent) % doors[i].Period == 0))
                return false;
        }

        return true;
    }
}

public class Door
{
    public Number Period { get; set; }
    public Number TimePassed { get; set; }
    public Number Index { get; set; }
    public Number Independent
    {
        get;set;
    }
}

public static class Utils
{
    /// <summary>
    /// Euclidean Greatest common divisor
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Number GCD(Number a , Number b, LinkedList<EuclideanResult> result = null)
    {
        Number aAbs = a >= 0 ? a : -a;
        Number bAbs = b >= 0 ? b: -b;

        Number max = aAbs > bAbs ? aAbs : bAbs;
        Number min = aAbs > bAbs ? bAbs : aAbs;

        return GCD_Aux(max, min, result);
    }

    /// <summary>
    /// Euclidean Auxiliary Greatest common divisor
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Number GCD_Aux(Number max, Number min, LinkedList<EuclideanResult> result = null)
    {
        if (min == 0)
        {
            if (result != null)
                result.AddLast(new EuclideanResult { Divisor = max });
            return max;
        }

        Number r = max % min; 

        if ( result !=null)
        {
            result.AddLast(new EuclideanResult { Dividend = max, Divisor = min, Cocient = max / min, Rest = r });
        }
        return GCD_Aux(min, r,result);
    }

    public static void GetEuclideanLinearEquation(LinkedList<EuclideanResult> gcdResult ,out Number a1, out Number a2 )
    {
        Number gcd = gcdResult.Last.Value.Divisor;
        Number max = gcdResult.Count >1 ? gcdResult.First.Value.Dividend : gcdResult.First.Value.Divisor;
        Number min = gcdResult.Count > 1 ? gcdResult.First.Value.Divisor : 0;

        if(min == 0 )
        {
            a1 = 1;
            a2 = 1;
            return;
        }

        if ( max == min)
        {
            a1 = 2;
            a2 = -1;
            return;
        }

        if ( min == gcd )
        {
            a1 = 1;
            a2 = gcdResult.First.Value.Cocient - 1;
            return;
        }

        var gcdResultArray = gcdResult.Skip(1).Take(gcdResult.Count - 3).ToArray();

        Number[] ri1 = new Number [] { 0, 1 };
        Number[] ri2 = new Number [] { 1, -gcdResult.First.Value.Cocient };

        for (Number i = 0; i < gcdResultArray.Length ;  i++)
        {
            Number[] riNew = new Number[] { ri1[0] - ri2[0] * gcdResultArray[i].Cocient,  ri1[1] - ri2[1] * gcdResultArray[i].Cocient };
            ri1 = ri2;
            ri2 = riNew;
        }

        a1 = ri2[0];
        a2 = ri2[1];
    }

    public static bool SolveDiophanLinearEquation(Number a1, Number a2, Number independent, out Door door1, out Door door2)
    {
        door1 = null;
        door2 = null;

        Number gcd = 0;
        LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();

        ////if (a1 > a2)
        ////    gcd = Utils.GCD_Aux(a1, a2, result);
        ////else
        ////    gcd = Utils.GCD_Aux(a2, a1, result);

        //if (Math.Abs(a1) > Math.Abs(a2))
        //    gcd = Utils.GCD(a1, a2, result);
        //else
        //    gcd = Utils.GCD(a2, a1, result);

        gcd = Utils.GCD(a1, a2, result);

        if (independent % gcd != 0)
            return false;
        Number a1Sol, a2Sol;
        if (Math.Abs(a1) > Math.Abs(a2))
            GetEuclideanLinearEquation(result, out a1Sol, out a2Sol);
        else
            GetEuclideanLinearEquation(result, out a2Sol, out a1Sol);

        Number cocient = independent / gcd;
        a1Sol *= cocient;
        a2Sol *= cocient;

        if (a1 < 0)
            a1Sol *= -1;
        if (a2 < 0)
            a2Sol *= -1;

        door1 = new Door { Period = a2/ gcd, Independent = -a1Sol  };
        door2 = new Door { Period = -a1 / gcd, Independent = -a2Sol};

        return true;
    }
}

public class EuclideanResult
{
    public Number Dividend { get; set; }
    public Number Divisor { get; set; }
    public Number Cocient { get; set; }
    public Number Rest { get; set; }
}

