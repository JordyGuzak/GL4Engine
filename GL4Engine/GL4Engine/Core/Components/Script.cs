
namespace GL4Engine.Core
{
    abstract class Script : Behaviour
    {
        public Script()
        {
        }

        public abstract void Start();
        public abstract void Update();
        public virtual void OnClose() { }
    }
}
