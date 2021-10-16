using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ScriptTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var script = ((LevelScript) new ExampleTest());
            var task = new Task(async () => {
                try
                {
                    await script.Execute(args);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
            task.Start();


            Console.ReadKey();
        }
    }
}
