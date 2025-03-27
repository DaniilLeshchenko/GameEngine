 using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using GameEngine.Engine.Core;

namespace Engine.Managers
{
    public sealed class CameraManager : GameComponent
    {
        private static Dictionary<string, CameraComponent> cameras;
        private static CameraComponent activeCamera;
        private static string activeCameraID;

        public static CameraComponent ActiveCamera
        {
            get { return activeCamera; }
        }

        public CameraManager(Game _game)
            : base(_game)
        {
            cameras = new Dictionary<string, CameraComponent>();

            _game.Components.Add(this);
        }

        public List<string> GetCurrentCameras()
        {
            return cameras.Keys.ToList();
        }

        public static void SetActiveCamera(string id)
        {
            if (cameras.ContainsKey(id))
            {
                if (activeCameraID != id)
                {
                    activeCamera = cameras[id];
                    activeCamera.Initialize();

                    activeCameraID = id;
                }
            }
        }

        public static void AddCamera(CameraComponent camera)
        {
            if (!cameras.ContainsKey(camera.ID))
            {
                cameras.Add(camera.ID, camera);

                if (cameras.Count == 1)
                    SetActiveCamera(camera.ID);
            }
        }

        public static void RemoveCamera(string id)
        {
            if (cameras.ContainsKey(id))
            {
                if (cameras.Count > 1)
                {
                    cameras.Remove(id);
                }
            }
        }

        public static void Clear()
        {
            cameras.Clear();
            activeCamera = null;
        }
    }
}
