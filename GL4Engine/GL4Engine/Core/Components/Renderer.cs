
namespace GL4Engine.Core
{
    abstract class Renderer : Behaviour
    {
        public abstract void Draw(Camera camera, Light[] lights);
    }
}
