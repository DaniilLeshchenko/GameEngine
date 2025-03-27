using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Engine.Core
{
    public class RenderComponent : Component
    {
        public virtual void Draw(CameraComponent camera) { }
    }
}
