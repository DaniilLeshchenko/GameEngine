using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GameEngine.Engine.Core
{

    public class GameObject
    {
        public string ID { get; private set; }

        public bool Enabled { get; set; }

        public TransformComponent Transform { get; set; } = new TransformComponent();

        private List<Component> components = new List<Component>();

        public delegate void GameObjectEventDelegate(GameObject component);

        public event GameObjectEventDelegate OnDestroy;

        public Scene Scene { get; set; }

        private bool isInitialised = false;

        public GameObject()
        {
            ID = GetType().Name + Guid.NewGuid();
        }

        public virtual void Initialize()
        {
            foreach (Component component in components)
            {
                component.Initialize();
            }

            isInitialised = true;
        }

        public virtual void PostInitialize()
        {
            foreach (Component component in components)
            {
                component.PostInitialize();
            }
        }

        public virtual void Update()
        {
            foreach (Component component in components)
            {
                if (component.Enabled)
                {
                    component.Update();
                }
            }
        }

        public void Draw(CameraComponent camera)
        {
            foreach (RenderComponent renderComponent in components)
            {
                if (renderComponent.Enabled)
                {
                    renderComponent.Draw(camera);
                }
            }
        }

        public void Destroy()
        {
            if (OnDestroy != null)
            {
                OnDestroy(this);
            }
        }

        public void RemoveComponent(Component component)
        {
            if (components.Contains(component))
            {
                components.Remove(component);
            }
        }

        public void AddComponent<T>(T component) where T : Component
        {
            component.GameObject = this;
            component.OnDestroy += RemoveComponent;

            if (isInitialised)
            {
                component.Initialize();
            }

            components.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)components.First(c => c is T);
        }

        public List<T> GetComponentss<T>() where T : Component
        {
            return components.FindAll(c => c is T) as List<T>;
        }

        public IEnumerable<RenderComponent> GetAllRenderers()
        {
            return components.OfType<RenderComponent>();
        }
    }
}
