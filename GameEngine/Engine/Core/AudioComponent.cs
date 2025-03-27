using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Engine.Core
{
    public class AudioComponent: Component
    {
        public virtual void Play()
        { }
        public virtual void Pause()
        { }
        public virtual void Stop()
        { }
    }
}
