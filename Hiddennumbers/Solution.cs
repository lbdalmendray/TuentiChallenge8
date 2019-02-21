using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Solution
{
    /// <summary>
    /// Explanation :   
    /// There are 25 differents cases from base 2 to base 26 . 
    /// Calculating each different case and put in code is not to difficult.
    /// First I calculate each case with Main2 to out25.txt. Then I copy-paste each answer
    /// into the Main code. 
    /// Result is the answer for each base.
    /// If there are T cases and T > 26 then is a fact that are repeated cases. 
    /// I have defined the InfiniteNumber class because I need to work with large numbers and different bases. 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        try
        {
            int T = int.Parse(Console.ReadLine());
            string[] result = new string[]
            {
                "empty",
                "empty",
                  "0"
                , "10"
                , "153"
                , "2236"
                , "36445"
                , "676950"
                , "14257425"
                , "337049848"
                , "8853086421"
                , "256025960290"
                , "8087635880665"
                , "277162583161140"
                , "10243482925323453"
                , "406178854419941806"
                , "17201909104707658785"
                , "774978678516192705520"
                , "37009642565164493858725"
                , "1867560225209938300461498"
                , "99295935468144044321329641"
                , "5548404150487432086262603180"
                , "325065432144121537060585277325"
                , "19925810223573405096759694695430"
                , "1275436487008769296112278161640753"
                , "85098841553745943302702572610643176"
                , "5908752853312064033482129579774948725"
            };
            for (int i = 1; i <= T; i++)
            {
                var S = Console.ReadLine();                
                string prefixString = "Case #" + i + ": ";
                Console.WriteLine(prefixString + result[S.Length]);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    public static void Main2(string[] args)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("out25.txt"))
            {
                writer.WriteLine("\"0\"");
                for (int i = 3; i <= 26; i++)
                {
                    var numberCrecient = new int[i].Select((v, index) => index).Reverse();
                    InfiniteNumber maxValue = new InfiniteNumber(numberCrecient.ToArray(), i);
                    var numberDecrecient = numberCrecient.Reverse().ToArray();
                    numberDecrecient[0] = 1;
                    numberDecrecient[1] = 0;
                    InfiniteNumber minValue = new InfiniteNumber(numberDecrecient, i);
                    var difference = (maxValue - minValue).ToBase10();
                    writer.WriteLine(", \"" + difference.ToString() + "\"");
                }
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
}




