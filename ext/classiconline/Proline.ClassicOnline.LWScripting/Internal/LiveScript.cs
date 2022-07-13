using CitizenFX.Core;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MScripting.Internal;
using Proline.Resource;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting
{
    internal class LiveScript
    { 
        public string Name => Instance.GetType().Name; 
        public CancellationTokenSource CancelToken => ScriptTaskTokenManager.GetInstance()[ExecutionTask]; 
        public bool IsCompleted => ExecutionTask != null ? ExecutionTask.IsCompleted : false;
        public Task ExecutionTask => ScriptTaskManager.GetInstance()[Instance]; 
        public int Id => ExecutionTask == null ? -1 : ExecutionTask.Id;
        public bool IsMarkedForNolongerNeeded { get; internal set; }
        public object Instance => ScriptInstanceManager.GetInstance()[_instanceId];
        public string InstanceId => _instanceId;
         
        private string _instanceId;

        public LiveScript(string instanceId)
        {
            _instanceId = instanceId;  
        } 

        public void Terminate()
        {
            if (!IsCompleted)
            { 
                CancelToken.Cancel();
            }
        }

        internal async Task Execute(CancellationTokenSource source, object instance, params object[] args)
        { 
            var terminationCode = 0;
            var type = instance.GetType();
            var name = type.Name; 
            try
            {
                var method = type.GetMethod("Execute");
                Console.WriteLine(string.Format("{0} Script Started", name, terminationCode));
                var invokationTask = (Task)method.Invoke(instance, new object[] { args, source.Token });
                while (!invokationTask.IsCompleted && !source.IsCancellationRequested)
                {
                    await BaseScript.Delay(1000);
                }
                Console.WriteLine(string.Format("{0} Completed", name));

                if (invokationTask.Exception != null)
                {
                    throw invokationTask.Exception;
                }
                terminationCode = 0;
            }
            catch (AggregateException e)
            {
                terminationCode = 2;
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                terminationCode = 1;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                Console.WriteLine(string.Format("{0} Script Finished, Termination Code: {1}", name, terminationCode));
            }
        }
    }
}
