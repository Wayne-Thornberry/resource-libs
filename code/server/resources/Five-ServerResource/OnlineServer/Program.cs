using System; 
using System.Collections.Generic;
using Proline.Resource.Eventing; 
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Proline.ClassicOnline.EventQueue;

namespace OnlineEngine
{
    public class Program
    {  
        public static void Main(string[] args)
        {
            Console.WriteLine($"Started Server");
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
