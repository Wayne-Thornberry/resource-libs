using Proline.Resource.Framework;

namespace CWorldObjects.Commands
{
    public class ExitProperty : ResourceCommand
    {
        public ExitProperty() : base("ExitProperty")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length == 3)
            {
                var api = new CWorldObjectsAPI();
                api.ExitProperty(args[0].ToString(), args[1].ToString(), args[2].ToString());
            }
        }
    }
}
