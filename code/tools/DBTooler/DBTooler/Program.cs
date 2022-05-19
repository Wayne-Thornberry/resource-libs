using Proline.DBAccess.Proxies;
using System;

namespace DBTooler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var x = new DBAccessClient())
            {
               var result = x.SaveFile(new PlacePlayerDataInParameters() { Data = "dsadasdsa" });
                while (!result.IsCompleted)
                {

                }
            } 
        }
    }
}
