using GL4Engine.Core;

namespace GL4Engine.Graphics.Shaders
{
    class DefaultShader : ShaderProgram
    {
        public DefaultShader(string name) : base(name, "vs_tex.glsl", "fs_tex.glsl", true)
        {
            
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

            SetTransformationMatrix(transform.GetTransformationMatrix());
            SetViewMatrix(camera.GetViewMatrix());
            SetProjectionMatrix(camera.ProjectionMatrix);
            UpdateMaterialUniforms(material);
            UpdateLightUniforms(lights);
        }

        public void UpdateMaterialUniforms(Material material)
        {
            SetVector3(GetUniform("material.diffuse"), material.DiffuseColor);
            SetVector3(GetUniform("material.specular"), material.SpecularColor);
            SetFloat(GetUniform("material.specular_exponent"), material.SpecularExponent);
        }

        public void UpdateLightUniforms(Light[] lights)
        {
            SetInt(GetUniform("numLights"), lights.Length);

            for(int i = 0; i < lights.Length; i++)
            {
                string str = "lights[" + i + "].";
                SetInt(GetUniform(str + "type"), (int)lights[i].Type);
                SetVector3(GetUniform(str + "position"), lights[i].transform.position);
                SetVector3(GetUniform(str + "color"), lights[i].Color);
                SetFloat(GetUniform(str + "attenuation"), lights[i].Attenuation);
                SetFloat(GetUniform(str + "ambientCoefficient"), lights[i].AmbientCoefficient);
                SetFloat(GetUniform(str + "coneAngle"), lights[i].ConeAngle);
                SetVector3(GetUniform(str + "coneDirection"), lights[i].ConeDirection);
            }
        }
    }
}
