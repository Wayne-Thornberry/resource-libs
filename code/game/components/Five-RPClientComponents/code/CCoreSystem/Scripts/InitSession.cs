using Proline.ClassicOnline.EventQueue;
using System.Threading.Tasks;

namespace CCoreSystem.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {
            ComponentEvent.InvokeEvent("CTestEventRandomInput");
            ComponentEvent.InvokeEvent("CPublicTestEventRandomInput");
            var api = new CCoreSystemAPI();
            api.StartNewScript("Main");
        }
    }
}
