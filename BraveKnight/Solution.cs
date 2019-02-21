using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Solution
{
    /// <summary>
    /// Explanation :  
    /// the BraveKnight class is the Main Class .
    /// The Idea is to make a Directed Graph with all the squares . 
    /// Each square is a vertex . There would be an edge (v1,v2) if the vertex v1 could go to vertex v2 in just one step.
    /// Each edge has the same value, then we could use a Breadth-first search and Shortest-Distance algorithm 
    /// when we want to calculate the shortest-path in steps from a vertex v to w .(Maybe it doesn't exist)
    /// The solution is calculating first the shortest-path from Start Position -> to Princess Position (if Exists) 
    /// then From Princess Position to Destination Position ( if exists) the firstDistance + secondDistance is the answer.
    /// In other case is IMPOSSIBLE. 
    /// graph G = (V = vertexs = N*M ,E = edges = O(V) ) 
    /// Each vertex has at most 8 edges => E = O(V) 
    /// this algorithm is O(V+E) = O(V) = O (N*M)  because E = O(V)  and BFS = O(V+E) 
    /// The result is O(C*N*M)
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
                    var line = Console.ReadLine();
                    string[] splitParts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int N = int.Parse(splitParts[0]);
                    int M = int.Parse(splitParts[1]);
                    string[] terrain = new string[N];
                    for (int j = 0; j < N; j++)
                    {
                        line = Console.ReadLine();
                        terrain[j] = line;
                    }
                    BraveKnight braveKnight = new BraveKnight(terrain);
                    string prefixString = "Case #" + i + ": ";
                    if (braveKnight.ExistShortestRescue(out int result))
                    {
                        Console.WriteLine(prefixString + result);
                    }
                    else
                    {
                        Console.WriteLine(prefixString + "IMPOSSIBLE");
                    }
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

public class BraveKnight
{
    private char[][] terrain;
    public Dictionary<int, int>[] graph;

    public Position Start { get; set; }
    public Position Destination { get; private set; }
    public Position Princess { get; private set; }
    public int RowCount { get; set; }
    public int ColumnCount { get; set; }

    public BraveKnight(string[] terrain)
    {
        this.terrain = terrain.Select(s => s.ToArray()).ToArray();
        RowCount = terrain.Length;
        ColumnCount = terrain[0].Length;
        Start = FindPosition('S');
        Destination = FindPosition('D');
        Princess = FindPosition('P');

        this.terrain[Start.Row][Start.Column] = '.';
        this.terrain[Destination.Row][Destination.Column] = '.';
        this.terrain[Princess.Row][Princess.Column] = '.';

        var edges = CreateEdges();
        graph = CreateGraph(RowCount * ColumnCount, edges);
    }

    public bool ExistShortestRescue(out int distance)
    {
        if (!ExistShortestGoingPrincess(out distance))
            return false;
        if (!ExistShortestGoingDestination(out int distance2))
            return false;
        distance += distance2;
        return true;
    }

    public bool ExistShortestGoingPrincess(out int distance)
    {
        var minDistance = MinDistance(GetPosVertexIndex(Start), GetPosVertexIndex(Princess));
        distance = minDistance.HasValue ? minDistance.Value : 0;
        return minDistance.HasValue;
    }

    public bool ExistShortestGoingDestination(out int distance)
    {
        var minDistance = MinDistance(GetPosVertexIndex(Princess), GetPosVertexIndex(Destination));
        distance = minDistance.HasValue ? minDistance.Value : 0;
        return minDistance.HasValue;
    }

    /// <summary>
    /// This is a Breadth-first search and Shortest-Distance algorithm 
    /// </summary>
    /// <param name="vertex1"></param>
    /// <param name="vertex2"></param>
    /// <returns></returns>
    public int? MinDistance(int vertex1 , int vertex2)
    {
        int[] distances = new int[RowCount * ColumnCount];
        bool[] vertexCalculated = new bool[RowCount * ColumnCount];
        vertexCalculated[vertex1] = true;

        LinkedList<int> vertexs = new LinkedList<int>();
        vertexs.AddLast(vertex1);

        while (vertexs.Count != 0)
        {
            var currentVertex = vertexs.First.Value;
            vertexs.RemoveFirst();
            foreach (var vertexEdge in graph[currentVertex].Keys.Where(v=>!vertexCalculated[v]))
            {
                vertexCalculated[vertexEdge] = true;
                distances[vertexEdge] = distances[currentVertex] + 1;
                if (vertex2 == vertexEdge)
                    return distances[vertexEdge];
                vertexs.AddLast(vertexEdge);
            }
        }

        return null;
    }

    public int[][] CreateEdges()
    {
        List<int[]> edges = new List<int[]>();
        int vertexIndex = 0;
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++,vertexIndex++)
            {
                int maxDistance;
                int minDistance;
                if (terrain[i][j] == '.')
                {
                    maxDistance = 2;
                    minDistance = 1;
                }
                else if (terrain[i][j] == '*')
                {
                    maxDistance = 4;
                    minDistance = 2;
                }
                else
                    continue;

                var upLeft = new Position { Row = i - maxDistance, Column = j - minDistance };
                if ( IsGoodPosition(upLeft))
                {
                    edges.Add(new int[] { vertexIndex, GetPosVertexIndex(upLeft) });
                }

                var upRight = new Position { Row = i - maxDistance, Column = j + minDistance };
                if (IsGoodPosition(upRight))
                {
                    edges.Add(new int[] { vertexIndex, GetPosVertexIndex(upRight) });
                }

                var rightUp = new Position { Row = i - minDistance , Column = j + maxDistance };
                if (IsGoodPosition(rightUp))
                {
                    edges.Add(new int[] { vertexIndex, GetPosVertexIndex(rightUp) });
                }

                var rightDown = new Position { Row = i + minDistance, Column = j + maxDistance };
                if (IsGoodPosition(rightDown))
                {
                    edges.Add(new int[] { vertexIndex, GetPosVertexIndex(rightDown) });
                }

                var downRight = new Position { Row = i + maxDistance, Column = j + minDistance };
                if (IsGoodPosition(downRight))
                {
                    edges.Add(new int[] { vertexIndex, GetPosVertexIndex(downRight) });
                }

                var downLeft = new Position { Row = i + maxDistance, Column = j - minDistance };
                if (IsGoodPosition(downLeft))
                {
                    edges.Add(new int[] { vertexIndex, GetPosVertexIndex(downLeft) });
                }

                var leftDown = new Position { Row = i + minDistance, Column = j - maxDistance };
                if (IsGoodPosition(leftDown))
                {
                    edges.Add(new int[] { vertexIndex, GetPosVertexIndex(leftDown) });
                }

                var leftUp = new Position { Row = i - minDistance, Column = j - maxDistance };
                if (IsGoodPosition(leftUp))
                {
                    edges.Add(new int[] { vertexIndex, GetPosVertexIndex(leftUp) });
                }
            }
        }        

        return edges.ToArray();
    }

    public int GetPosVertexIndex(Position p)
    {
        return p.Row * ColumnCount + p.Column;
    }

    private bool IsGoodPosition(Position position)
    {
        return position.Column >= 0 && position.Row >= 0 && position.Column < ColumnCount && position.Row < RowCount
            && terrain[position.Row][position.Column] != '#';
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

    Position FindPosition(char value)
    {
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                if (terrain[i][j] == value)
                {
                    return new Position { Row = i, Column = j };
                }
            }
        }
        return null;
    }
}

public class Position
{
    public int Row { get; set; }
    public int Column { get; set; }
}
