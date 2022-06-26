using System;

namespace Proline.Workbench.EventTester
{
    internal class Program
    {
        public delegate void DoX();
        public static event DoX DoXHandler;

        static void Main(string[] args)
        {
            DoXHandler += 
        }
    }
}
