using GameEngine.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    public class Player : GameObject
    {
        private float moveSpeed = 0.2f;

        public Player(Vector3 location)
            : base()
        {
            Transform.SetPosition(location);
        }

        public override void Initialize()
        {
            AddComponent(new CameraComponent());

            base.Initialize();
        }

        public override void Update()
        {
            Vector3 right = Vector3.Cross(Transform.World.Forward, Transform.World.Up);
            right.Normalize();

            if (InputManager.IsKeyHeld(Keys.W))
               Transform.SetPosition(Transform.Position + Transform.World.Forward * moveSpeed);
            else if (InputManager.IsKeyHeld(Keys.S))
                Transform.SetPosition(Transform.Position - Transform.World.Forward * moveSpeed);
            
            if (InputManager.IsKeyHeld(Keys.A))
                Transform.SetPosition(Transform.Position - right * moveSpeed);
            else if (InputManager.IsKeyHeld(Keys.D))
                Transform.SetPosition(Transform.Position + right * moveSpeed);
            
            if (InputManager.IsKeyHeld(Keys.Space))
                Transform.SetPosition(Transform.Position + Transform.World.Up * moveSpeed);
            else if (InputManager.IsKeyHeld(Keys.LeftControl))
                Transform.SetPosition(Transform.Position - Transform.World.Up * moveSpeed);

            base.Update();
        }
    }
}
