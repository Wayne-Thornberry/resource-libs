using CCoreSystem.Internal;
using Proline.Resource.Framework;
using Console = Proline.Resource.Console;

namespace CCoreSystem.Commands
{
    public class ListAllTasksCommand : ResourceCommand
    {
        public ListAllTasksCommand() : base("ListAllTasks")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            foreach (var scriptTask in TaskManager.Tasks)
            {
                Console.WriteLine(string.Format("Task Id {0}, Is Complete {1}, Status {2} ", scriptTask.Id, scriptTask.IsCompleted, scriptTask.Status));
            }
        }
    }
}
