using System.Drawing;
using CitizenFX.Core;

namespace Proline.CFXExtended.Core
{
    public abstract class Marker : ISpatial
    {
        protected Marker(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Type = MarkerType.VerticalCylinder;
            Color = Color.White;
        }

        protected Marker(Vector3 position, Vector3 rotation, Vector3 scale, MarkerType type)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Type = type;
            Color = Color.White;
        }

        protected Marker(Vector3 position, Vector3 rotation, Vector3 scale, MarkerType type, Color color)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Type = type;
            Color = color;
        }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public MarkerType Type { get; set; }
        public Color Color { get; set; }
        //public bool IsInMarker => World.GetDistance(Game.PlayerPed.Position, Position) < Scale.X;

        public void Draw()
        {
            World.DrawMarker(Type, Position, Rotation, Rotation, Scale, Color);
        }
    }
}