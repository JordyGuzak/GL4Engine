using GL4Engine.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace GL4Engine.Core
{
    class MeshRenderer : Renderer
    {
        public Mesh Mesh { get; private set; }
        public Material Material { get; set; }

        public MeshRenderer(Mesh mesh, Material material)
        {
            Mesh = mesh;
            Material = material;
        }

        public override void Draw(Camera camera, Light[] lights)
        {
            Mesh.Shader.UpdateUniforms(camera, transform, Material, lights);
            GL.DrawElements(BeginMode.Triangles, Mesh.VertexCount, DrawElementsType.UnsignedInt, 0);
        }
    }
}
