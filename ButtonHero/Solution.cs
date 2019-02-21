using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Solution
{
    /// <summary>
    /// Explanation : 
    /// The NoteProblem class is the main class.
    /// The idea is a dynamic programming solution . 
    /// If we sort the notes by the time1(that is the start arriving time ) we could see this problem 
    /// like Note1 Note2 ---- NoteN and we want to obtain the MaximumScore from Note1 to the Final 
    /// If We see this problem like having the set Note1 Note2 --- NoteN it could be the problem 
    /// of MaximumScore from Note_i to the final with i = 1 
    /// The we have a generic problem MaximumScore(i) .
    /// The MaximumScore(i) = MaximumValue( MaximumScore(selecting(Note_i)), MaximumScore(NotSelecting(Note_i));
    /// MaximumScore(selecting(Note_i)) = NoteValue_i.Score + MaximumScore(j) with j the first index Note of all the Notes that are not intersecting
    /// with Note_i
    /// MaximumScore(NotSelecting(Note_i) = MaximumScore(i+1) 
    /// This algorithm is O(N*log(N)) foreach case
    /// The Total is O(C*O(N*log(N))) 
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
                    int N = int.Parse(Console.ReadLine());
                    LinkedList<Note> notes = new LinkedList<Note>();
                    for (int j = 0; j < N; j++)
                    {
                        string[] stringParts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int X = int.Parse(stringParts[0]);
                        int L = int.Parse(stringParts[1]);
                        int S = int.Parse(stringParts[2]);
                        int P = int.Parse(stringParts[3]);
                        notes.AddLast(new Note { Position = X , Length = L, Speed = S , Score = P  });
                    }

                    int result = NoteProblem.Solve(notes.ToArray());
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

public class Note
{
    public int Position { get; set; }
    public int Length { get; set; }
    public int Speed { get; set; }
    public int Score { get; set; }
    int? time1 = null;
    public int Time1
    {
        get
        {
            if (!time1.HasValue)
                time1 = Position / Speed;
            return time1.Value;
        }
    }
    int? time2;
    public int Time2
    {
        get
        {
            if (!time2.HasValue)
                time2 = time1 + Length / Speed;
            return time2.Value;
        }
    }


    public bool FirstIntersect(Note note)
    {
        // this.TIme1 <= note.Time1 // Constraints

        return note.time1 <= this.Time2;
    }
}

public static class NoteProblem
{
    public static int Solve(Note [] notes)
    {
        notes = MergeEquivalents(notes);
        Array.Sort(notes.Select(n => n.Time1).ToArray(), notes);

        bool[] calculated = new bool[notes.Length];
        int[] maximums = new int[notes.Length];

        Calculate(notes, 0, calculated, maximums);

        return maximums.Max(m => m);
    }

    private static int Calculate(Note[] notes, int index, bool[] calculated, int[] maximums)
    {
        if (calculated[index])
            return maximums[index];

        int result1 = notes[index].Score;

        int firstIndex = index+1;
        //firstIndex = NIntersectionSearch(firstIndex, notes[index], notes);
        firstIndex = BinaryIntersectionSearch(firstIndex, notes.Length - 1, notes[index], notes);

        if (firstIndex < notes.Length)
        {
           result1 += Calculate(notes, firstIndex, calculated, maximums);
        }

        var result2 = index + 1 < notes.Length ? Calculate(notes, index + 1, calculated, maximums) : 0;

        var result = result1 > result2 ? result1 : result2;

        calculated[index] = true;
        maximums[index] = result;
        return result;
    }

    private static int NIntersectionSearch(int firstIndex, Note note ,Note[] notes)
    {
        for (; firstIndex < notes.Length; firstIndex++)
        {
            if (!note.FirstIntersect(notes[firstIndex]))
                return firstIndex;
        }

        return firstIndex;
    }

    private static int BinaryIntersectionSearch(int index1, int index2, Note note, Note[] notes)
    {
        if (index1 >= notes.Length)
            return notes.Length;

        while (index2 - index1 > 1)
        {
            int medIndex = (index2 + index1) / 2;
            if (!note.FirstIntersect(notes[medIndex]))
            {
                index2 = medIndex;
            }
            else
                index1 = medIndex + 1;
        }

        if (!note.FirstIntersect(notes[index1]))
        {
            return index1;
        }
        if (!note.FirstIntersect(notes[index2]))
        {
            return index2;
        }

        return notes.Length;
    }

    public static Note [] MergeEquivalents(Note [] notes)
    {
        var groupGroups = notes.GroupBy(n => n.Time1).Select(g => g.GroupBy(e => e.Time2));
        LinkedList<IGrouping<int, Note>> list = new LinkedList<IGrouping<int, Note>>();
        foreach (var groupGroup in groupGroups)
        {
            foreach (var group in groupGroup)
            {
                list.AddLast(group);
            }
        }

        Note[] result = new Note[list.Count];

        var node = list.First;
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = node.Value.First();
            result[i].Score = node.Value.Sum(n => n.Score);
            node = node.Next;
        }
        
        return result;
    }
}
