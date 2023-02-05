
using Proline.Resource.Framework;

namespace CWorldObjects.Commands
{
    public class EnterProperty : ResourceCommand
    {
        public EnterProperty() : base("EnterProperty")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length == 3)
            {
                var api = new CWorldObjectsAPI();
                api.EnterProperty(args[0].ToString(), args[1].ToString(), args[2].ToString());
            }
        }
    }
}
