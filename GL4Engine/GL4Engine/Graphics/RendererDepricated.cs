using GL4Engine.Core;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace GL4Engine.Graphics
{
    class RendererDepricated
    {
        //private Camera mainCamera;

        //public RendererDepricated(Camera camera, StaticShader shader)
        //{
        //    mainCamera = camera;
        //    shader.Start();
        //    shader.LoadProjectionMatrix(mainCamera.ProjectionMatrix);
        //    shader.Stop();
        //}

        //public void Init()
        //{
        //    GL.Enable(EnableCap.DepthTest);
        //    GL.Enable(EnableCap.Texture2D);
        //    //GL.Enable(EnableCap.CullFace); GL.CullFace(CullFaceMode.Back);
        //    //GL.Enable(EnableCap.Blend); GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrc1Alpha);
        //}

        //public void Prepare()
        //{
        //    GL.ClearColor(Color.Red);
        //    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        //}

        //public void Render(TexturedModel texturedModel, StaticShader shader)
        //{
        //    shader.Start();
        //    RawModel model = texturedModel.RawModel;
        //    GL.BindVertexArray(model.VAO);
        //    GL.EnableVertexAttribArray(0);
        //    GL.EnableVertexAttribArray(1);
        //    Matrix4 transformationMatrix = texturedModel.transform.GetTransformationMatrix();
        //    shader.LoadViewMatrix(mainCamera);
        //    shader.LoadTransformationMatrix(transformationMatrix);
        //    GL.ActiveTexture(TextureUnit.Texture0);
        //    GL.BindTexture(TextureTarget.Texture2D, texturedModel.Texture.TextureID);
        //    GL.DrawElements(BeginMode.Triangles, model.VertexCount, DrawElementsType.UnsignedInt, 0);
        //    GL.DisableVertexAttribArray(0);
        //    GL.DisableVertexAttribArray(1);
        //    GL.BindVertexArray(0);
        //    shader.Stop();
        //}
    }
}
