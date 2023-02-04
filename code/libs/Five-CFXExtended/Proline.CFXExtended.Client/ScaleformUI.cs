using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Scaleforms
{
    public sealed class ScaleformArgumentTXD
    {
        #region Fields
        internal string _txd;
        #endregion

        public ScaleformArgumentTXD(string s)
        {
            _txd = s;
        }
    }

    public abstract class ScaleformUI : IDisposable
    {
        private readonly string _scaleformID;
        private int _handle;
        private int _handle2;
        private int _handle3;
        private int _handle4;
        private int _handle5;

        public ulong NativeValue => (ulong)Handle;
        public bool IsValid => Handle != 0;
        public bool IsLoaded => API.HasScaleformMovieLoaded(_handle) || API.HasScaleformMovieLoaded(_handle2) || API.HasScaleformMovieLoaded(_handle3);
        public int Handle => _handle;

        public ScaleformUI(string scaleformId)
        {
            _scaleformID = scaleformId;
        }

        ~ScaleformUI()
        {
            Dispose();
        }

        protected void CallFunction(string function, params object[] arguments)
        {
            API.BeginScaleformMovieMethod(Handle, function);
            if (arguments.Length > 0 && arguments != null)
            {
                foreach (var argument in arguments)
                {
                    if (argument is int)
                    {
                        API.PushScaleformMovieMethodParameterInt((int)argument);
                    }
                    else if (argument is string)
                    {
                        API.PushScaleformMovieMethodParameterString((string)argument);
                    }
                    else if (argument is char)
                    {
                        API.PushScaleformMovieMethodParameterString(argument.ToString());
                    }
                    else if (argument is float)
                    {
                        API.PushScaleformMovieMethodParameterFloat((float)argument);
                    }
                    else if (argument is double)
                    {
                        API.PushScaleformMovieMethodParameterFloat((float)(double)argument);
                    }
                    else if (argument is bool)
                    {
                        API.PushScaleformMovieMethodParameterBool((bool)argument);
                    }
                    else if (argument is ScaleformArgumentTXD)
                    {
                        API.PushScaleformMovieMethodParameterString(((ScaleformArgumentTXD)argument)._txd);
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format("Unknown argument type {0} passed to scaleform with handle {1}.",
                                argument.GetType().Name, Handle), "arguments");
                    }
                }
            }
            API.EndScaleformMovieMethod();
        }

        protected async Task<T> CallFunction<T>(string function)
        {
            API.BeginScaleformMovieMethod(Handle, function);
            var functionHandle = API.EndScaleformMovieMethodReturn();
            while (!API.IsScaleformMovieMethodReturnValueReady(functionHandle))
                await BaseScript.Delay(0);
            //Debug.WriteLine(typeof(T).Name);
            switch (typeof(T).Name.ToLower())
            {
                case "int32":
                    return (T)Convert.ChangeType(API.GetScaleformMovieMethodReturnValueInt(functionHandle), typeof(T));
                case "bool":
                    return (T)Convert.ChangeType(API.GetScaleformMovieMethodReturnValueBool(functionHandle), typeof(T));
                case "string":
                    return (T)Convert.ChangeType(API.GetScaleformMovieMethodReturnValueString(functionHandle), typeof(T));
                default:
                    //Debug.WriteLine("Returned Default");
                    return default;
            }
        }

        public async Task Load()
        {
            if (IsLoaded) return;
            _handle = API.RequestScaleformMovie(_scaleformID);
            _handle2 = API.RequestScaleformMovie_2(_scaleformID);
            _handle3 = API.RequestScaleformMovie3(_scaleformID);
            _handle4 = API.RequestScaleformMovieInstance(_scaleformID);
            _handle5 = API.RequestScaleformMovieInteractive(_scaleformID);

            //Debug.WriteLine(
            //_handle + " " +
            //_handle2 + " " +
            //_handle3 + " " +
            //_handle4 + " " +
            //_handle5);
            while (!IsLoaded)
            {
                //Debug.WriteLine("Loading...");
                await BaseScript.Delay(0);
            }
        }

        public async Task Unload()
        {
            if (!IsLoaded) return;
            API.SetScaleformMovieAsNoLongerNeeded(ref _handle);
            while (IsLoaded) await BaseScript.Delay(0);
        }

        public void Dispose()
        {
            if (IsLoaded)
            {
                API.SetScaleformMovieAsNoLongerNeeded(ref _handle);
            }
            GC.SuppressFinalize(this);
        }

        public void Render2D()
        {
            API.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 0);
        }

        public void Render2DScreenSpace(PointF location, PointF size)
        {
            float x = location.X / Screen.Width;
            float y = location.Y / Screen.Height;
            float width = size.X / Screen.Width;
            float height = size.Y / Screen.Height;

            API.DrawScaleformMovie(Handle, x + width / 2.0f, y + height / 2.0f, width, height, 255, 255, 255, 255,
                0);
        }

        public void Render3D(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            API.DrawScaleformMovie_3dNonAdditive(Handle, position.X, position.Y, position.Z, rotation.X, rotation.Y,
                rotation.Z, 2.0f, 2.0f, 1.0f, scale.X, scale.Y, scale.Z, 2);
        }

        public void Render3DAdditive(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            API.DrawScaleformMovie_3d(Handle, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z,
                2.0f, 2.0f, 1.0f, scale.X, scale.Y, scale.Z, 2);
        }

    }
}