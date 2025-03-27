using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

//Author: Daniil Leshchenko
// 27.03.25
// Description:This class defines an object's position, rotation,
// and scale in 3D space. It also calculates the world transformation matrix based on those values.

namespace GameEngine.Engine.Core
{
    public class TransformComponent : Component
    {
        public Vector3 Position { get; private set; } = Vector3.Zero;

        public Quaternion Rotation { get; private set; } = Quaternion.Identity;

        public Vector3 Scale { get; private set; } = Vector3.One;

        public Matrix worldMatrix;

        public Matrix World
        {
            get
            {
                worldMatrix =
                    Matrix.CreateScale(Scale) *
                    Matrix.CreateFromQuaternion(Rotation) *
                    Matrix.CreateTranslation(Position);

                return worldMatrix;
            }
        }

        public void SetPosition(Vector3 newPosition)
        {
            Position = newPosition;
        }

        public void SetRotation(Quaternion newRotation)
        {
            Rotation = newRotation;
        }

        public void SetScale(Vector3 newScale)
        {
            Scale = newScale;
        }
    }
}
