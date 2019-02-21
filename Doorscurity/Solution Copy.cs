using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                        int P = int.Parse(Console.ReadLine());
                        int T = int.Parse(Console.ReadLine());
                        doors.AddLast(new Door { Period = P, TimePassed = T});
                    }
                    Doorscurity doorscurity = new Doorscurity(doors.ToArray());
                    string prefixString = "Case #" + i + ": ";

                    if (doorscurity.Solve(out int result))
                        Console.WriteLine(prefixString + result);
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
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Index = i;
        }
    }

    public Door[] Doors { get; private set; }

    public bool Solve(out int result)
    {
        result = 0;

        foreach (var door in Doors)
        {
            if (door.TimePassed == door.Period)
                door.TimePassed = 0;

            door.Independent = -((door.Period - door.TimePassed) % door.Period) + door.Index;
            //door.Independent = -door.Period + door.TimePassed + door.Index;
        }

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
        if (!CreateInterval(doorsConstraints, out int minIntValue, out int maxIntValue))
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

    private bool CreateInterval(LinkedList<Door> doorsConstraints, out int minIntValue, out int maxIntValue)
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
                min = Math.Max(min, ((double)doorsConstraint.Independent) / doorsConstraint.Period);
            }
            else
            {
                max = Math.Min(max, ((double)doorsConstraint.Independent) / doorsConstraint.Period);
            }
            if (min > max)
                return false;
        }

        minIntValue = (int)min;
        maxIntValue = (int)max;

        return true;
    }
}

public class Door
{
    public int Period { get; set; }
    public int TimePassed { get; set; }
    public int Index { get; set; }
    public int Independent
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
    public static int GCD(int a , int b)
    {
        int aAbs = a >= 0 ? a : -a;
        int bAbs = b >= 0 ? b: -b;

        int max = aAbs > bAbs ? aAbs : bAbs;
        int min = aAbs > bAbs ? bAbs : aAbs;

        return GCD_Aux(max, min);
    }

    /// <summary>
    /// Euclidean Auxiliary Greatest common divisor
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static int GCD_Aux(int max, int min, LinkedList<EuclideanResult> result = null)
    {
        if (min == 0)
        {
            if (result != null)
                result.AddLast(new EuclideanResult { Divisor = max });
            return max;
        }

        int r = max % min; 

        if ( result !=null)
        {
            result.AddLast(new EuclideanResult { Dividend = max, Divisor = min, Cocient = max / min, Rest = r });
        }
        return GCD_Aux(min, r,result);
    }

    public static void GetEuclideanLinearEquation(LinkedList<EuclideanResult> gcdResult ,out int a1, out int a2 )
    {
        int gcd = gcdResult.Last.Value.Divisor;
        int max = gcdResult.Count >1 ? gcdResult.First.Value.Dividend : gcdResult.First.Value.Divisor;
        int min = gcdResult.Count > 1 ? gcdResult.First.Value.Divisor : 0;

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

        int[] ri1 = new int [] { 0, 1 };
        int[] ri2 = new int [] { 1, -gcdResult.First.Value.Cocient };

        for (int i = 0; i < gcdResultArray.Length ;  i++)
        {
            int[] riNew = new int[] { ri1[0] - ri2[0] * gcdResultArray[i].Cocient,  ri1[1] - ri2[1] * gcdResultArray[i].Cocient };
            ri1 = ri2;
            ri2 = riNew;
        }

        a1 = ri2[0];
        a2 = ri2[1];
    }

    public static bool SolveDiophanLinearEquation(int a1, int a2, int independent, out Door door1, out Door door2)
    {
        door1 = null;
        door2 = null;

        int gcd = 0;
        LinkedList<EuclideanResult> result = new LinkedList<EuclideanResult>();

        if (a1 > a2)
            gcd = Utils.GCD_Aux(a1, a2, result);
        else
            gcd = Utils.GCD_Aux(a2, a1, result);

        if (independent % gcd != 0)
            return false;
        int a1Sol, a2Sol;
        if (a1 > a2)
            GetEuclideanLinearEquation(result, out a1Sol, out a2Sol);
        else
            GetEuclideanLinearEquation(result, out a2Sol, out a1Sol);

        int cocient = independent / gcd;
        a1Sol *= cocient;
        a2Sol *= cocient;

        door1 = new Door { Period = a2 / gcd, Independent = -a1Sol };
        door2 = new Door { Period = a1 / gcd, Independent = -a2Sol };

        return true;
    }
}

public class EuclideanResult
{
    public int Dividend { get; set; }
    public int Divisor { get; set; }
    public int Cocient { get; set; }
    public int Rest { get; set; }
}

