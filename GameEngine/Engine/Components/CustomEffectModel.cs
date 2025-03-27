using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Engine.Core;

namespace GameEngine.Engine.Components
{
    internal class CustomEffectModel : RenderComponent
    {
        private string modelAssetName;
        private string effectAssetName;
        private string textureAssetName;

        protected Model ModelAsset;
        protected Effect EffectAsset;
        protected Texture2D TextureAsset;

        public Matrix World { get; set; }
        private Matrix[] BoneTransforms;


        public Vector3 DiffuseColor { get; set; } = Vector3.One;
        public Vector3 AmbientColor { get; set; } = new Vector3(0.3f);
        public Vector3 LightDirection { get; set; } = Vector3.Normalize(new Vector3(1, 1, 1));
        public Vector3 LightColor { get; set; } = Vector3.One;
        public bool TextureEnabled => TextureAsset != null;

        public CustomEffectModel(string modelName, string effectName, Matrix transform)
        {
            modelAssetName = modelName;
            effectAssetName = effectName;
            World = transform;
        }

        public CustomEffectModel(string modelName, string effectName, string textureName, Matrix transform)
        {
            modelAssetName = modelName;
            effectAssetName = effectName;
            textureAssetName = textureName;
            World = transform;
        }

        public virtual void LoadContent(ContentManager content)
        {
            ModelAsset = content.Load<Model>(modelAssetName);
            BoneTransforms = new Matrix[ModelAsset.Bones.Count];
            ModelAsset.CopyAbsoluteBoneTransformsTo(BoneTransforms);

            if (!string.IsNullOrEmpty(textureAssetName))
            {
                TextureAsset = content.Load<Texture2D>(textureAssetName);
            }

            EffectAsset = content.Load<Effect>(effectAssetName);

            foreach (ModelMesh mesh in ModelAsset.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = EffectAsset;
                }
        }

        public virtual void Update() { }

        public virtual void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in ModelAsset.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    SetEffectParameter(effect, "World", BoneTransforms[mesh.ParentBone.Index] * World);
                    SetEffectParameter(effect, "View", view);
                    SetEffectParameter(effect, "Projection", projection);

                    SetEffectParameters(effect);
                }
                mesh.Draw();
            }
        }

        protected virtual void SetEffectParameters(Effect effect)
        {
            SetEffectParameter(effect, "DiffuseColor", DiffuseColor);
            SetEffectParameter(effect, "AmbientColor", AmbientColor);
            SetEffectParameter(effect, "LightDirection", LightDirection);
            SetEffectParameter(effect, "LightColor", LightColor);
            SetEffectParameter(effect, "TextureEnabled", TextureEnabled);

            if (TextureEnabled)
            {
                SetEffectParameter(effect, "rocks", TextureAsset);
            }
        }

        private void SetEffectParameter(Effect effect, string paramName, object value)
        {
            if (effect.Parameters[paramName] == null)
                return;

            if (value is Vector3 vec3)
                effect.Parameters[paramName].SetValue(vec3);
            else if (value is Matrix mat)
                effect.Parameters[paramName].SetValue(mat);
            else if (value is bool b)
                effect.Parameters[paramName].SetValue(b);
            else if (value is Texture2D tex)
                effect.Parameters[paramName].SetValue(tex);
            else if (value is float f)
                effect.Parameters[paramName].SetValue(f);
            else if (value is int i)
                effect.Parameters[paramName].SetValue(i);
        }
    }
}
