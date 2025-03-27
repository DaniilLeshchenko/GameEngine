using GameEngine.Objects;
using Microsoft.Xna.Framework;
using GameEngine.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Engine.Core;

namespace GameEngine
{
    public class TestScene : Scene
    {
        public TestScene() : base(Vector3.Zero, 500) 
        {
        
        }
        public override void Initialize()
        {
            Add(new Player(new Vector3(0, 1.8f, 10)));
            Add(new BasicModelObject(new Vector3(0, 0, 0), "SM_Cube"));

            base.Initialize();
        }
    }


}
