using GL4Engine.Core;
using OpenTK;
using OpenTK.Graphics.OpenGL4;


namespace GL4Engine.Graphics
{
    class Mesh
    {
        private int vao = -1;
        public int VAO { get { return vao; } }

        private int vertexCount = 0;
        public int VertexCount { get { return vertexCount; } }

        private ShaderProgram shader;
        public ShaderProgram Shader { get { return shader; } set { shader = value; } }

        // Add shader to Mesh

        public Mesh(int vao, int vertexCount, ShaderProgram shader = null)
        {
            this.vao = vao;
            this.vertexCount = vertexCount;
            this.shader = shader != null ? shader : Resources.Instance.LoadShader("default");
        }

        private void CalculateNormals(float[] vertices, uint[] indices)
        {
            for (int i = 0; i < indices.Length; i+=3)
            {
                uint i0 = indices[i];
                uint i1 = indices[i + 1];
                uint i2 = indices[i + 2];

                Vector3 v1 = new Vector3(vertices[i], vertices[i + 1], vertices[i + 2]);

            }
        }


        public void Bind()
        {
            GL.BindVertexArray(vao);
            shader.Start();
            shader.EnableVertexAttribArrays();
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
            shader.DisableVertexAttribArrays();
            shader.Stop();
        }
    }
}
