using System;
using System.Collections.Generic;

public class Solution
{
    /// <summary>
    /// Explanation :
    /// Each hole is determined for Two consecutive-horizontal lines and two consecutive-vertical lines.
    /// There are M-1 pairs of consecutive-horizontal lines .
    /// There are N-1 pairs of consecutive-vertical lines .
    /// For each of the N-1 pairs of this consecutive-horizontal lines we can use any pair of the vertical ones and make a hole.
    /// Then there are (N-1)*(M-1) holes 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        try
        {
            int C = int.Parse(Console.ReadLine());
            for (int i = 1; i <= C; i++)
            {
                var splitParts = Console.ReadLine().Split(' ');
                int N = int.Parse(splitParts[0]);
                int M = int.Parse(splitParts[1]);
                var result = (N - 1) * (M - 1);
                string prefixString = "Case #" + i + ": ";
                Console.WriteLine(prefixString + result);
            }
        }
        catch(Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}
