using System;
using GL4Engine.Graphics;
using System.Collections.Generic;


namespace GL4Engine.Core
{
    sealed class Resources
    {
        private static readonly Resources instance = new Resources();

        public static Resources Instance
        {
            get
            {
                return instance;
            }
        }

        private Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();
        private Dictionary<string, ShaderProgram> shaders = new Dictionary<string, ShaderProgram>();
        private Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        private Loader loader;

        private Resources()
        {
            loader = new Loader();
        }

        public void AddShader(ShaderProgram shader)
        {
            if (shaders.ContainsKey(shader.Name))
            {
                throw new ArgumentException($"Shader with name '{shader.Name}' already exists.");
            }

            shaders.Add(shader.Name, shader);
        }


        public ShaderProgram LoadShader(string name)
        {
            if (!shaders.ContainsKey(name))
            {
                throw new ArgumentException($"Shader with name '{name}' does not exist. You should add the shader first.");
            }

            return shaders[name];
        }

        public Texture LoadTexture(string filename)
        {
            if (textures.TryGetValue(filename, out Texture texture))
            {
                return texture;
            }
            else
            {
                texture = loader.LoadTexture(filename);
                textures.Add(filename, texture);
            }

            return texture;
        }

        public Mesh LoadMesh(string filename)
        {
            if (meshes.TryGetValue(filename, out Mesh mesh))
            {
                return mesh;
            }
            else
            {
                mesh = loader.LoadMesh(filename);
                meshes.Add(filename, mesh);
            }

            return mesh;
        }

        public Mesh LoadPrimitive(Primitive primitive)
        {
            return loader.LoadToVAO(primitive.GetVertices(), primitive.GetIndices(), primitive.GetTextureCoords(), null);
        }
    }
}
