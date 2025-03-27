using GameEngine.Engine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Engine.Core
{
    public class CameraComponent : Component
    {
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        public BoundingFrustum Frustum { get { return new BoundingFrustum(View * Projection); } }

        public float NearPlane { get; set; } = 0.1f;
        public float FarPlane { get; set; } = 1000f;
        public float AspectRatio { get; set; }
        public float FOV { get; set; } = 80;

        public override void Initialize()
        {
            UpdateViewMatrix();
            UpdateProjectionMatrix();

            CameraManager.AddCamera(this);

            base.Initialize();
        }

        public override void Update()
        {
            UpdateViewMatrix();

            base.Update();
        }

        protected virtual void UpdateViewMatrix()
        {
            View = Matrix.CreateLookAt(
                GameObject.Transform.Position,
                GameObject.Transform.Position + GameObject.Transform.World.Forward,
                GameObject.Transform.World.Up);
        }

        protected virtual void UpdateProjectionMatrix()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(FOV),
                (float)Utilities.GraphicsDevice.Viewport.Width / Utilities.GraphicsDevice.Viewport.Height,
                NearPlane,
                FarPlane);
        }
    }
}
