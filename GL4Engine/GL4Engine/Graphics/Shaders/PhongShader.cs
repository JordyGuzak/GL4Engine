using GL4Engine.Core;
using OpenTK;

namespace GL4Engine.Graphics.Shaders
{
    class PhongShader : ShaderProgram
    {
        public Vector3 AmbientLight { get; set; }

        public PhongShader(string name) : base(name, "vs_phong.glsl", "fs_phong.glsl", true)
        {
            AmbientLight = Vector3.One;
        }

        public override void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "textureCoord");
            BindAttribute(2, "normal");
        }

        public override void UpdateUniforms(Camera camera, Transform transform, Material material, Light[] lights)
        {
            if (material.Texture != null)
                material.Texture.Bind();
            else
                RenderUtil.UnbindTexture2D();

            SetModelViewProjectionMatrix(transform.GetTransformationMatrix(), camera.GetViewMatrix(), camera.ProjectionMatrix);
            SetVector3(GetUniform("diffuseColor"), material.DiffuseColor);
            SetVector3(GetUniform("ambientLight"), AmbientLight);
        }
    }
}
