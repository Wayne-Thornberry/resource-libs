using CitizenFX.Core;
using Proline.EngineFramework.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.EngineFramework
{
    public abstract class EngineScript : BaseScript
    {
        private CancellationTokenSource _source;
        private CancellationToken _token;
        private int _terminationCode;
        private Log _log;
        protected int Stage { get; set; }

        internal string ScriptName { get; set; }
        internal object[] Parameters { get; set; }

        public EngineScript()
        {
            _source = new CancellationTokenSource();
            _token = _source.Token;
            _log = new Log();
            Tick += OnTick;
        }

        public virtual async Task Execute(object[] args, CancellationToken token) { }

        protected void LogDebug(object x)
        {
            _log.Debug(x.ToString());
        }

        private async Task OnTick()
        {
            try
            {
                _log.Debug(string.Format("{0} Script Started", ScriptName, _terminationCode));
                await Execute(Parameters, _token);
                _terminationCode = 0;
            }
            catch(ScriptTerminatedException e)
            {
                _terminationCode = 2; 
            }
            catch (Exception e)
            {
                _terminationCode = 1;
                _log.Error(e.ToString());
            }
            finally
            {
                _log.Debug(string.Format("{0} Script Finished, Termination Code: {1}", ScriptName, _terminationCode));
                Tick -= OnTick;
            }
        }

        protected void StartNewScript(string scriptName, params object[] args)
        {
            try
            {
                var assembly = Assembly.GetCallingAssembly();
                foreach (var item in assembly.GetTypes())
                {
                    //_log.Debug(item.Name);
                    if (item.Name.Equals(scriptName))
                    {
                        var scripType = item;
                        var scriptInstance = (EngineScript)Activator.CreateInstance(scripType, new object[] { scripType.Name });
                        scriptInstance.Parameters = args;
                        RegisterScript(scriptInstance);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                _log.Debug(string.Format("Unable to start {0}",scriptName));
                _log.Error(e.ToString());
            }
        }

        /// <summary>
        /// Throws a contrelled terminated exception to exit out of the script completely
        /// </summary>
        protected internal void TerminateScript()
        {
            throw new ScriptTerminatedException();
        }

        /// <summary>
        /// Marks a script as no longer needed via the cancelation token provided.
        /// </summary>
        protected internal void MarkScriptAsNoLongerNeeded()
        {
            _source.Cancel();
        }
    }
}
