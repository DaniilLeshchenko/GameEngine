using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Engine.Core
{
    public abstract class Scene
    {
        private OcTree ocTree;
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> gameObjectsToRemove = new List<GameObject>();
        private List<RenderComponent> visibleEntities = new List<RenderComponent>();
        private bool isInitialised = false;

        public Scene(Vector3 worldCenter, float worldSize)
        {
            ocTree = new OcTree(worldCenter, worldSize);
        }

        public virtual void Initialize()
        {
            for (int i = 0; i < gameObjects.Count; i++) 
            {
                gameObjects[i].Initialize();
                AddRenderersToOctree(gameObjects[i]);
            }

            isInitialised = true;
        }

        public virtual void PostInitialize()
        {
            for (int i = 0;i < gameObjects.Count;i++)
            {
                gameObjects[i].PostInitialize();
            }
        }

        private void AddRenderersToOctree (GameObject gameObject)
        {
            foreach (RenderComponent renderComponent in gameObject.GetAllRenderers()) 
            {
                ocTree.AddObject(renderComponent);  
            }
        }

        public void Remove(GameObject gameObject)
        {
            if (!gameObjectsToRemove.Contains(gameObject))
            {
                gameObjectsToRemove.Add(gameObject);
            }
        }

        public void Add(GameObject gameObject)
        {
            gameObject.OnDestroy += Remove;

            if (isInitialised)
            {
                gameObject.Scene = this;
                gameObject.Initialize();
                AddRenderersToOctree(gameObject);
                gameObject.PostInitialize();
            }

            gameObjects.Add(gameObject);
        }

        public void Update(GameTime gameTime)
        {
            for(int i = 0; i < gameObjects.Count;i++) 
            {
                gameObjects[i].Update();
            }

            if(gameObjectsToRemove.Count > 0)
            {
                for (int i = 0; i < gameObjectsToRemove.Count;i++)
                {
                    gameObjects.Remove(gameObjectsToRemove[i]); 
                }

                gameObjectsToRemove.Clear();
            }
        }

        public void Draw(CameraComponent camera)
        {
            ocTree.ProcessTree(camera.Frustum, visibleEntities);

            foreach (var entity in visibleEntities) 
            {
                entity?.Draw(camera);
            }
        }
    }
}
