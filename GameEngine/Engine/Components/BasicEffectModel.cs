﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Engine.Core;

namespace GameEngine.Engine.Components
{
    public class BasicEffectModel : RenderComponent
    {
        private string asset;

        public Model Model { get; set; }
        public Matrix[] boneTransforms;

        public BasicEffectModel(string asset)
            : base()
        {
            this.asset = asset;
        }

        public override void Initialize()
        {
            if (!string.IsNullOrEmpty(asset))
            {
                Model = Utilities.Content.Load<Model>("Models/" + asset);
                boneTransforms = new Matrix[Model.Bones.Count];
                Model.CopyAbsoluteBoneTransformsTo(boneTransforms);
            }

            base.Initialize();
        }

        public override void Draw(CameraComponent camera)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.PreferPerPixelLighting = true;

                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                    effect.World = boneTransforms[mesh.ParentBone.Index] * GameObject.Transform.World;
                    effect.EnableDefaultLighting();
                }

                mesh.Draw();
            }

            base.Draw(camera);
        }
    }
}
