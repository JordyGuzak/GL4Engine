using OpenTK.Graphics.OpenGL4;

namespace GL4Engine.Graphics
{
    class Texture
    {
        private int textureID = -1;
        public int TextureID { get { return textureID; } }

        private int width;
        public int Width { get { return width; } }

        private int height;
        public int Height { get { return height; } }

        public Texture(int textureID, int width, int height)
        {
            this.textureID = textureID;
            this.width = width;
            this.height = height;
        }

        public void Bind()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textureID);
        }

        public void Unbind()
        {

        }
    }
}
