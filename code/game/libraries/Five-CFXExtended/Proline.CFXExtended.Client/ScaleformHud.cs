using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System.Drawing;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Scaleforms
{
    public class ScaleformHud
    {
        private int _scaleformId;

        public bool IsLoaded => API.HasHudScaleformLoaded(_scaleformId);

        public int Handle { get; private set; }

        public ScaleformHud(int scaleformId)
        {
            _scaleformId = scaleformId;
            Handle = scaleformId;
        }

        public async Task Load()
        {
            if (API.HasHudScaleformLoaded(_scaleformId)) return;
            API.RequestHudScaleform(_scaleformId);
            while (!IsLoaded) await BaseScript.Delay(0);
            CallFunction("READY", _scaleformId);
        }

        public void CallFunction(string function, params object[] args)
        {
            API.BeginScaleformMovieMethodHudComponent(_scaleformId, function);
            if (args.Length > 0)
                foreach (var arg in args)
                {
                    var type = arg.GetType();
                    if (type == typeof(bool)) API.PushScaleformMovieMethodParameterBool((bool)arg);
                    else if (type == typeof(float)) API.PushScaleformMovieMethodParameterFloat((float)arg);
                    else if (type == typeof(int)) API.PushScaleformMovieMethodParameterInt((int)arg);
                    else if (type == typeof(string)) API.PushScaleformMovieMethodParameterString((string)arg);
                }
            API.EndScaleformMovieMethod();
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