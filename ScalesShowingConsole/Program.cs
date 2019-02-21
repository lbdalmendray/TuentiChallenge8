using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalesShowingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ScaleSolver scaleSolver = new ScaleSolver();
            foreach (var majorScale in scaleSolver.majorScales)
            {
                Console.WriteLine(majorScale.Key + "   ");
                var nodes = majorScale.Value.Select((v, i) => new { v, i }).Where(v => v.v).Select((v) => scaleSolver.keys.First(n => n.Index == v.i));
                foreach (var node in nodes)
                {
                    Console.Write(node.Name + " ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
