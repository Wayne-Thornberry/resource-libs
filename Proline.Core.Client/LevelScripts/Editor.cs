using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.Engine;

namespace Proline.Freemode.LevelScripts
{
    public class Editor : LevelScript
    {
        private Camera _cam;
        private float _cameraSensitivity;
        private Entity _lastSelectedEntity;
        private float _movementSpeed;
        private RaycastResult _raycastResult;
        private Entity _selectedEntity;

        public Editor()
        {

        }

        public override async Task Execute(params object[] args)
        {
            _cam = World.CreateCamera(Game.PlayerPed.Position, new Vector3(0, 0, 0), 70);
            World.RenderingCamera = _cam;
            Game.PlayerPed.IsVisible = false;
            Game.PlayerPed.IsPositionFrozen = true;
            Game.PlayerPed.IsInvincible = true;
            Game.PlayerPed.CanRagdoll = false;
            Screen.Hud.IsRadarVisible = false;

            _cameraSensitivity = 10f;
            _movementSpeed = 5f;

            while (Stage != -1)
            {
                Game.PlayerPed.Position = _cam.Position;
                _raycastResult = World.Raycast(_cam.Position, _cam.UpVector, 100f, IntersectOptions.Everything);
                if (_selectedEntity == null)
                    Screen.ShowSubtitle("");
                else
                    Screen.ShowSubtitle(GetSelectedEntity());

                if (_selectedEntity != null)
                    World.DrawMarker(MarkerType.DebugSphere, _selectedEntity.Position, new Vector3(0, 0, 0),
                        new Vector3(0, 0, 0), new Vector3(0.2f, 0.2f, 0.2f), Color.FromArgb(150, 255, 0, 0));
                World.DrawMarker(MarkerType.VerticalCylinder, _raycastResult.HitPosition, new Vector3(0, 0, 0),
                    new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.FromArgb(150, 255, 255, 255));

                EditorControls();
                CameraRotation();
                CameraMovement();
            }
        }

        private void EditorControls()
        {
            Game.DisableControlThisFrame(0, Control.Aim);
            if (Game.IsControlJustPressed(0, Control.Aim))
            {
                if (_raycastResult.DitHit)
                {
                    if (_raycastResult.DitHitEntity)
                    {
                        ChangeEntity(_raycastResult.HitEntity);
                    }
                    else
                    {
                        if (World.GetDistance(_raycastResult.HitPosition,
                                World.GetClosest(_raycastResult.HitPosition, World.GetAllProps()).Position) < 5f)
                            ChangeEntity(World.GetClosest(_raycastResult.HitPosition, World.GetAllProps()));
                        else
                            ChangeEntity(null);
                    }
                }

                if (Game.IsControlPressed(0, Control.FrontendAccept))
                    if (_raycastResult.DitHit)
                        Screen.ShowNotification(_raycastResult.HitPosition.ToString());
            }

            if (Game.IsControlJustPressed(0, Control.FrontendPause))
            {
                World.RenderingCamera = null;
                Game.PlayerPed.IsVisible = true;
                Game.PlayerPed.IsInvincible = false;
                Game.PlayerPed.CanRagdoll = true;
                Game.PlayerPed.IsPositionFrozen = false;
                Screen.Hud.IsRadarVisible = true;
            }
        }

        private void ChangeEntity(Entity entity)
        {
            if (_selectedEntity != null)
            {
                _selectedEntity.Opacity = 255;
                _lastSelectedEntity = _selectedEntity;
            }

            _selectedEntity = entity;
            if (_selectedEntity != null)
                _selectedEntity.Opacity = 150;
        }

        private string GetSelectedEntity()
        {
            return _selectedEntity.Model.Hash + "\n" + (uint)_selectedEntity.Model.Hash + "\n";
        }

        private void CameraRotation()
        {
            var x = Game.GetControlNormal(0, Control.LookLeftRight);
            var y = Game.GetControlNormal(0, Control.LookUpDown);

            x *= -1;
            y *= -1;
            _cam.Rotation = new Vector3(_cam.Rotation.X + y * _cameraSensitivity, 0,
                _cam.Rotation.Z + x * _cameraSensitivity);
        }

        private void CameraMovement()
        {
            if (Game.IsControlPressed(0, Control.SniperZoomInOnly))
            {
                if (_movementSpeed >= 1000) return;
                _movementSpeed += 1f;
            }
            else if (Game.IsControlPressed(0, Control.SniperZoomOutOnly))
            {
                if (_movementSpeed <= 0) return;
                _movementSpeed -= 1f;
            }

            // Up and Down
            if (Game.IsControlPressed(0, Control.VehicleFlyThrottleUp))
                _cam.Position -= _cam.ForwardVector * Game.LastFrameTime * _movementSpeed;
            else if (Game.IsControlPressed(0, Control.VehicleFlyThrottleDown))
                _cam.Position += _cam.ForwardVector * Game.LastFrameTime * _movementSpeed;


            // Forward and Back
            if (Game.IsControlPressed(0, Control.MoveUpOnly))
                _cam.Position += _cam.UpVector * Game.LastFrameTime * _movementSpeed;
            else if (Game.IsControlPressed(0, Control.MoveDown))
                _cam.Position -= _cam.UpVector * Game.LastFrameTime * _movementSpeed;

            // Left and Right
            if (Game.IsControlPressed(0, Control.MoveRight))
                _cam.Position += _cam.RightVector * Game.LastFrameTime * _movementSpeed;
            else if (Game.IsControlPressed(0, Control.MoveLeftOnly))
                _cam.Position -= _cam.RightVector * Game.LastFrameTime * _movementSpeed;
        }
    }
}