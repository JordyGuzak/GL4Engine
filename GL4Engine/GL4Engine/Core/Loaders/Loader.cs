using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using GL4Engine.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace GL4Engine.Core
{
    class Loader : IDisposable
    {
        public string ResourcesFolder { get; private set; }

        private List<int> vaos = new List<int>();
        private List<int> vbos = new List<int>();
        private List<int> textures = new List<int>();

        private OBJLoader objLoader;

        public Loader()
        {
            ResourcesFolder = ResourceFolder.Path;
            objLoader = new OBJLoader(ResourcesFolder);
        }

        public Mesh LoadMesh(string filename)
        {
            return objLoader.LoadMesh(filename, this);
        }

        public Mesh LoadToVAO(float[] vertices, uint[] indices, float[] textureCoords, float[] normals)
        {
            int vaoID = CreateVAO();
            BindIndicesVBO(indices);
            StoreDataInAttributeList(0, 3, vertices);
            StoreDataInAttributeList(1, 2, textureCoords);
            StoreDataInAttributeList(2, 3, normals);
            UnbindVAO();
            return new Mesh(vaoID, indices.Length);
        }

        private int CreateVAO()
        {
            int vaoID = GL.GenVertexArray();
            vaos.Add(vaoID);
            GL.BindVertexArray(vaoID);
            return vaoID;
        }

        private void StoreDataInAttributeList(int attributeNumber, int size, float[] data)
        {
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * sizeof(float)), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeNumber, size, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void BindIndicesVBO(uint[] indices)
        {
            int iboID = GL.GenBuffer();
            vbos.Add(iboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(uint)), indices, BufferUsageHint.StaticDraw);
        }

        private void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }

        public Texture LoadTexture(string filename)
        {
            try
            {
                Bitmap bitmap = new Bitmap($@"{ResourcesFolder}Textures\" + filename);
                return LoadTexture(bitmap);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public Texture LoadTexture(Bitmap bitmap)
        {
            int textureID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, textureID);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            textures.Add(textureID);

            return new Texture(textureID, bitmap.Width, bitmap.Height);
        }

        public void CleanUp()
        {
            foreach (int vao in vaos)
            {
                GL.DeleteVertexArray(vao);
            }

            foreach (int vbo in vbos)
            {
                GL.DeleteBuffer(vbo);
            }

            foreach(int texture in textures)
            {
                GL.DeleteTexture(texture);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CleanUp();
            }
        }
    }
}
