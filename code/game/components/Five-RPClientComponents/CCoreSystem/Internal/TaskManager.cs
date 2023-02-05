using CitizenFX.Core;
using Proline.ClassicOnline.EventQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCoreSystem.Internal
{

    internal class TaskManager
    {
        private static List<Task> _runningTasks;
        public static List<Task> Tasks
        {
            get
            {
                if (_runningTasks == null)
                    _runningTasks = new List<Task>();
                return _runningTasks;
            }
        }

        internal IEnumerable<Task> GetAllTasks()
        {
            return Tasks;
        }

        internal static Task StartNew(Func<Task> taskFunc)
        {
            UpdateTaskList();
            var task = Task.Factory.StartNew(taskFunc);
            Tasks.Add(task);
            return task;
        }

        internal static Task StartNew(Action taskFunc)
        {
            UpdateTaskList();
            var task = Task.Factory.StartNew(taskFunc);
            Tasks.Add(task);
            return task;
        }

        private static void UpdateTaskList()
        {
            var tasks = Tasks.ToArray();
            var count = 0;
            foreach (var task in tasks)
            {
                if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.WaitingForActivation)
                {
                    Tasks.Remove(task);
                    count++;
                }
            }
        }
    }
}
