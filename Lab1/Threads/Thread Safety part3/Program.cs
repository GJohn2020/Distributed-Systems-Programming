using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Safety_part3
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadRunner runner = new ThreadRunner();
            runner.Run();

            Console.ReadLine(); // keep console open
        }
    }
}
