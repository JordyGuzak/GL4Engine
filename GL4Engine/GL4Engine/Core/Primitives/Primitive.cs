
namespace GL4Engine.Core
{
    abstract class Primitive
    {
        public abstract float[] GetVertices();
        public abstract uint[] GetIndices();
        public abstract float[] GetTextureCoords();
    }

}
