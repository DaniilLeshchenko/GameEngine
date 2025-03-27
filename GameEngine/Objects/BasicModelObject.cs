using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Engine.Core;
using GameEngine.Engine.Components;
using Microsoft.Xna.Framework;

namespace GameEngine.Objects
{
    public class BasicModelObject : GameObject
    {
        private string asset;

        public BasicModelObject(Vector3 location, string asset): base()
        {
            Transform.SetPosition(location);
            this.asset = asset;
        }

        public override void Initialize() 
        {
            AddComponent(new BasicEffectModel(asset));
            base.Initialize();
        }
    }
}
