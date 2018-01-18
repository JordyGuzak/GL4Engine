using OpenTK.Graphics.OpenGL4;

namespace GL4Engine.Graphics
{
    class RenderUtil
    {
        public static void EnableTexture2D()
        {
            GL.Enable(EnableCap.Texture2D);
        }

        public static void DisableTexture2D()
        {
            GL.Disable(EnableCap.Texture2D);
        }

        public static void UnbindTexture2D()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
