using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Solution
{
    /// <summary>
    /// Explanation : 
    /// ScaleSolver class is the main class .
    /// The idea is verify that each note of each SetNoteInput is in scale with a key.
    /// If we verify this for each scale with a key (major scale or minor scale) then
    /// the result would be just all the scale key that contains all the notes as main notes.
    /// O(C * Notes.Length) time 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        try
        {
            // O(1) time -- because it is just one time and each ScalSolver initialization
            // execute the same sentences in the same order . 
            ScaleSolver scaleSolver = new ScaleSolver();

            int C = int.Parse(Console.ReadLine());
            for (int i = 1; i <= C; i++)
            {
                try
                {
                    int N = int.Parse(Console.ReadLine());
                    string result = "";
                    if (N > 0)
                    {
                        var line = Console.ReadLine();
                        string[] Notes = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        // O(Notes.Length) time -- because
                        var scaleSolverResult = scaleSolver.GetKeys(Notes);
                        if (scaleSolverResult.Length == 0)
                            result = "None";
                        else
                        {
                            result += scaleSolverResult[0];
                            for (int j = 1; j < scaleSolverResult.Length; j++)
                            {
                                result += " " + scaleSolverResult[j];
                            }
                        }
                    }
                    else
                    {
                        result = "MA MA# MB MC MC# MD MD# ME MF MF# MG MG# mA mA# mB mC mC# mD mD# mE mF mF# mG mG#";
                    }
                    string prefixString = "Case #" + i + ": ";
                    Console.WriteLine(prefixString + result);
                }
                catch(Exception exception)
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
/// <summary>
/// Main Class
/// </summary>
public class ScaleSolver
{
    public Dictionary<string, bool[]> majorScales = new Dictionary<string, bool[]>();
    public Dictionary<string, bool[]> minorScales = new Dictionary<string, bool[]>();
    public Node[] keys;
    public Dictionary<string, int> keyIndex = new Dictionary<string, int>();

    public ScaleSolver()
    {
        //"C C# D D# E F F# G G# A A# B"
        CreateKeys();
        CreateScales();
    }
    /// <summary>
    /// Create the Keys and the relative order.
    /// </summary>
    private void CreateKeys()
    {
        Node C = new Node { Name = "C", Index = 0 };
        Node CSharp = new Node { Name = "C#", Index = 1 };
        Node D = new Node { Name = "D", Index = 2 };
        Node DSharp = new Node { Name = "D#", Index = 3 };
        Node E = new Node { Name = "E", Index = 4 };
        Node F = new Node { Name = "F", Index = 5 };
        Node FSharp = new Node { Name = "F#", Index = 6 };
        Node G = new Node { Name = "G", Index = 7 };
        Node GSharp = new Node { Name = "G#", Index = 8 };
        Node A = new Node { Name = "A", Index = 9 };
        Node ASharp = new Node { Name = "A#", Index = 10 };
        Node B = new Node { Name = "B", Index = 11 };

        keyIndex.Add(C.Name, C.Index);
        keyIndex.Add(CSharp.Name, CSharp.Index);
        keyIndex.Add(D.Name, D.Index);
        keyIndex.Add(DSharp.Name, DSharp.Index);
        keyIndex.Add(E.Name, E.Index);
        keyIndex.Add(F.Name, F.Index);
        keyIndex.Add(FSharp.Name, FSharp.Index);
        keyIndex.Add(G.Name, G.Index);
        keyIndex.Add(GSharp.Name, GSharp.Index);
        keyIndex.Add(A.Name, A.Index);
        keyIndex.Add(ASharp.Name, ASharp.Index);
        keyIndex.Add(B.Name, B.Index);

        C.Next = CSharp;
        CSharp.Next = D;
        D.Next = DSharp;
        DSharp.Next = E;
        E.Next = F;
        F.Next = FSharp;
        FSharp.Next = G;
        G.Next = GSharp;
        GSharp.Next = A;
        A.Next = ASharp;
        ASharp.Next = B;
        B.Next = C;

        keys = new Node[]
        {
            C,CSharp,D,DSharp,E,F,FSharp,G,GSharp,A,ASharp,B
        };
    }
    /// <summary>
    /// Create Each Scale with a Key 
    /// </summary>
    private void  CreateScales()
    {
        foreach (var key in keys.OrderBy(k=>k.Name))
        {
            var scale = CreateScale(key, new int[] { 2, 2, 1, 2, 2, 2, 1 });
            majorScales.Add("M"+key.Name, scale);

            scale = CreateScale(key, new int[] { 2, 1, 2, 2, 1, 2, 2 });
            minorScales.Add("m" + key.Name, scale);
        }
    }

    private bool [] CreateScale(Node key, int[] scale)
    {
        bool[] result = new bool[12];
        int currentIndex = key.Index;
        result[currentIndex] = true;
        for (int i = 0; i < scale.Length; i++)
        {
            currentIndex = currentIndex + scale[i];
            if (currentIndex > result.Length - 1)
                currentIndex = currentIndex - result.Length ;
            result[currentIndex] = true;            
        }

        return result;
    }

    public string [] GetKeys(string [] notes)
    {
        var normalizedNotes = NormalizeNotes(notes);
        var noteGroups = normalizedNotes.GroupBy(n => n).Select(g=>g.Key).ToArray();

        LinkedList<string> result = new LinkedList<string>();

        foreach (var majorScale in majorScales)
        {
            if(IsScaleCompatible(majorScale.Value, noteGroups))
            {
                result.AddLast(majorScale.Key);
            }
        }

        foreach (var minorScale in minorScales)
        {
            if (IsScaleCompatible(minorScale.Value, noteGroups))
            {
                result.AddLast(minorScale.Key);
            }
        }

        return result.ToArray();       
    }

    private bool IsScaleCompatible(bool[] scale, string[] noteGroups)
    {
        return noteGroups.All(note => scale[keyIndex[note]]);        
    }

    private string [] NormalizeNotes(string[] notes)
    {
        LinkedList<string> result = new LinkedList<string>();
        foreach (var note in notes)
        {
            switch (note)
            {
                case "Cb":
                    result.AddLast("B");
                    break;
                case "Db":
                    result.AddLast("C#");
                    break;
                case "Eb":
                    result.AddLast("D#");
                    break;
                case "Fb":
                    result.AddLast("E");
                    break;
                case "Gb":
                    result.AddLast("F#");
                    break;
                case "Ab":
                    result.AddLast("G#");
                    break;
                case "Bb":
                    result.AddLast("A#");
                    break;
                ///////////////////

                case "E#":
                    result.AddLast("F");
                    break;
                case "B#":
                    result.AddLast("C");
                    break;
                default:
                    result.AddLast(note);
                    break;
            }
        }

        return result.ToArray();
    }
}

public class Node
{
    public string Name { get; set; }
    public Node Next { get; set; }
    public int Index { get; set; }
}


