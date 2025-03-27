using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Engine.Core
{
    public delegate void ComponentEventDelegate(Component component);
    public abstract class Component
    {
        
        public string ID { get; private set; }

        public GameObject GameObject { get; set; }

        public bool Enabled { get; set; } = true;

        public event ComponentEventDelegate OnDestroy;

        protected Component()
        {

            ID = this.GetType().Name + Guid.NewGuid();

        }

        protected Component(string id)
        {

            ID = id;

        }

        public virtual void Initialize() { }

        public virtual void Update() { }

        public virtual void PostInitialize() { }

        public virtual void Destroy()
        {
            if(OnDestroy != null)
            {
                OnDestroy(this);
            }
        }
    }
}
