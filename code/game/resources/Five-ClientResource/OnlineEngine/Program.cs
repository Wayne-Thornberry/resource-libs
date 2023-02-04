using Proline.ClassicOnline.EventQueue;
using Proline.Resource;
using System;
using Console = Proline.Resource.Console;

namespace OnlineEngine
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine($"Started Engine");
            try
            {
                Component.InitializeComponents();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
