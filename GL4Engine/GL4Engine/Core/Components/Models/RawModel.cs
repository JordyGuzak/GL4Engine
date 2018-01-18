
namespace GL4Engine.Core
{
    class RawModel
    {
        public int VAO { get; set; }
        public int VertexCount { get; set; }

        public RawModel(int vao_id, int vertexCount)
        {
            VAO = vao_id;
            VertexCount = vertexCount;
        }
    }
}
