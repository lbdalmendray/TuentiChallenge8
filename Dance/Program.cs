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
                    string[] stringParts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int P = int.Parse(stringParts[0]);
                    int G = int.Parse(stringParts[1]);
                    LinkedList<int[]> edges = new LinkedList<int[]>();
                    for (int j = 0; j < G; j++)
                    {
                        stringParts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int A = int.Parse(stringParts[0]);
                        int B = int.Parse(stringParts[1]);
                        edges.AddLast(new int[] { A, B });
                    }
                    DanceResolver danceSolver = new DanceResolver(P, edges.ToArray());
                    int result = danceSolver.Solve();
                    string prefixString = "Case #" + i + ": ";
                    Console.WriteLine(prefixString + result);                                       
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

public class DanceResolver
{
    private int N;
    private int[][] edges;

    public DanceResolver(int N, int[][] edges)
    {
        this.N = N;
        this.edges = edges;
    }

    public int Solve()
    {
        if (N % 2 == 1)
            return 0;

        var graph = CreateGraph(N, edges);

        int[,] result = new int[N,N];
        bool[,] isCalculated = new bool[N, N];

        Calculate(0, N - 1, result, isCalculated, graph);


        return 0;
    }

    private int Calculate(int index1, int index2, int[,] result,bool [,] isCalculated, Dictionary<int, int>[] graph)
    {
        if (index1 > index2)
            return 0;

        if (index1 > result.Length - 1 || index2 > result.Length - 1)
            return 0 ;
        
        if ( isCalculated[index1,index2])
        {
            return result[index1, index2];
        }

        if( (index2 - index1+1) % 2 == 1)
        {
            isCalculated[index1, index2] = true;
            result[index1, index2] = 0;
        }

        if ( index2-index1 == 1 )
        {
            isCalculated[index1, index2] = true;
            result[index1, index2] = 1;
        }

        for (int i = index1; i <= index2; i++)
        {
            int result1 = CalculateDif(index1, i, index1,index2, result, isCalculated, graph);
            int result2 = CalculateDif(i+1, index2,0,0, result, isCalculated, graph); /// puede que mal
        }


        /// r
        /// 
        return 0;
    }

    private int CalculateDif(int index1, int index2,int min, int max,  int[,] result, bool[,] isCalculated, Dictionary<int, int>[] graph)
    {
        return 0;
    }

    Dictionary<int, int>[] CreateGraph(int n, int[][] edges)
    {
        Dictionary<int, int>[] result = new Dictionary<int, int>[n];

        for (int i = 0; i < n; i++)
        {
            result[i] = new Dictionary<int, int>(8);
        }

        for (int i = 0; i < edges.Length; i++)
        {
            var edge = edges[i];
            var vertex1 = edge[0];
            var vertex2 = edge[1];

            if (vertex1 == vertex2)
            {
                continue;
            }

            if (!result[vertex1].ContainsKey(vertex2))
            {
                result[vertex1].Add(vertex2, 1);
            }
        }

        return result;
    }
}
