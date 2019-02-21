using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFile
{
    class Program
    {
        static void Main(string[] args)
        {
           var line = Console.ReadLine();
           while(line != null)
           {
                Console.WriteLine(line);
                line = Console.ReadLine();
           }
        }
    }
}
