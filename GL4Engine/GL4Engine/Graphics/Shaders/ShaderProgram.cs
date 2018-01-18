using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System.Text;
using GL4Engine.Core;

namespace GL4Engine.Graphics
{
    abstract class ShaderProgram : IDisposable
    {
        public string Name { get; set; }
        public int ProgramID = -1;
        public int vertexShaderID = -1;
        public int fragmentShaderID = -1;
        public int AttributeCount = 0;
        public int UniformCount = 0;

        public Dictionary<string, AttributeInfo> Attributes = new Dictionary<string, AttributeInfo>();
        public Dictionary<string, UniformInfo> Uniforms = new Dictionary<string, UniformInfo>();
        public Dictionary<String, uint> Buffers = new Dictionary<string, uint>();

        public ShaderProgram(string name, string vertexShader, string fragmenShader, bool fromFile = true)
        {
            Name = name;
            ProgramID = GL.CreateProgram();

            if (fromFile)
            {
                LoadShaderFromFile(vertexShader, ShaderType.VertexShader);
                LoadShaderFromFile(fragmenShader, ShaderType.FragmentShader);
            }
            else
            {
                LoadShaderFromString(vertexShader, ShaderType.VertexShader);
                LoadShaderFromString(fragmenShader, ShaderType.FragmentShader);
            }

            Attach();
            BindAttributes();
            Link();
            GenBuffers();
        }


        public abstract void BindAttributes();
        public abstract void UpdateUniforms(Camera camera, Transform transform, Material material, Light[] lights);

        protected int GetUniformLocation(string uniformVariableName)
        {
            return GL.GetUniformLocation(ProgramID, uniformVariableName);
        }

        public void Start()
        {
            GL.UseProgram(ProgramID);

        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        public void BindAttribute(int attribute, string variableName)
        {
            GL.BindAttribLocation(ProgramID, attribute, variableName);
        }        

        private void loadShader(string code, ShaderType type, out int address)
        {
            address = GL.CreateShader(type);
            GL.ShaderSource(address, code);
            GL.CompileShader(address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        public void LoadShaderFromString(String code, ShaderType type)
        {
            if (type == ShaderType.VertexShader)
            {
                loadShader(code, type, out vertexShaderID);
            }
            else if (type == ShaderType.FragmentShader)
            {
                loadShader(code, type, out fragmentShaderID);
            }
        }

        public void LoadShaderFromFile(String filename, ShaderType type)
        {
            using (StreamReader sr = new StreamReader(ResourceFolder.Path + $@"Shaders\{filename}"))
            {
                if (type == ShaderType.VertexShader)
                {
                    loadShader(sr.ReadToEnd(), type, out vertexShaderID);
                }
                else if (type == ShaderType.FragmentShader)
                {
                    loadShader(sr.ReadToEnd(), type, out fragmentShaderID);
                }
            }
        }

        public void Attach()
        {
            GL.AttachShader(ProgramID, vertexShaderID);
            GL.AttachShader(ProgramID, fragmentShaderID);
        }

        public void Link()
        {
            GL.LinkProgram(ProgramID);

            Console.WriteLine(GL.GetProgramInfoLog(ProgramID));

            GL.GetProgram(ProgramID, GetProgramParameterName.ActiveAttributes, out AttributeCount);
            GL.GetProgram(ProgramID, GetProgramParameterName.ActiveUniforms, out UniformCount);

            for (int i = 0; i < AttributeCount; i++)
            {
                AttributeInfo info = new AttributeInfo();
                int length = 0;

                StringBuilder name = new StringBuilder();

                GL.GetActiveAttrib(ProgramID, i, 256, out length, out info.size, out info.type, name);

                info.name = name.ToString();
                info.address = GL.GetAttribLocation(ProgramID, info.name);
                Attributes.Add(name.ToString(), info);
            }

            for (int i = 0; i < UniformCount; i++)
            {
                UniformInfo info = new UniformInfo();
                int length = 0;

                StringBuilder name = new StringBuilder();

                GL.GetActiveUniform(ProgramID, i, 256, out length, out info.size, out info.type, name);

                info.name = name.ToString();
                Uniforms.Add(name.ToString(), info);
                info.address = GL.GetUniformLocation(ProgramID, info.name);
            }
        }

        public void GenBuffers()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                uint buffer = 0;
                GL.GenBuffers(1, out buffer);

                Buffers.Add(Attributes.Values.ElementAt(i).name, buffer);
            }

            for (int i = 0; i < Uniforms.Count; i++)
            {
                uint buffer = 0;
                GL.GenBuffers(1, out buffer);

                Buffers.Add(Uniforms.Values.ElementAt(i).name, buffer);
            }
        }

        public void EnableVertexAttribArrays()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                GL.EnableVertexAttribArray(Attributes.Values.ElementAt(i).address);
            }
        }

        public void DisableVertexAttribArrays()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                GL.DisableVertexAttribArray(Attributes.Values.ElementAt(i).address);
            }
        }

        public void SetModelViewProjectionMatrix(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            int address = GetUniform("model_view_projection");
            if (address == -1)
            {
                Console.WriteLine("Uniform 'model_view_projection' not found.");
                return;
            }

            Matrix4 model_view_projection = model * view * projection;

            SetMatrix(address, model_view_projection);
        }

        public void SetViewMatrix(Matrix4 view)
        {
            int address = GetUniform("view");
            if (address == -1) return;

            SetMatrix(address, view);
        }

        public void SetTransformationMatrix(Matrix4 tranformation)
        {
            int address = GetUniform("transformation");
            if (address == -1) return;

            SetMatrix(address, tranformation);
        }

        public void SetProjectionMatrix(Matrix4 projection)
        {
            int address = GetUniform("projection");
            if (address == -1) return;

            SetMatrix(address, projection);
        }

        public int GetAttribute(string name)
        {
            if (Attributes.ContainsKey(name))
            {
                return Attributes[name].address;
            }
            else
            {
                return -1;
            }
        }

        public int GetUniform(string name)
        {
            if (Uniforms.ContainsKey(name))
            {
                return Uniforms[name].address;
            }
            else
            {
                return -1;
            }
        }

        public uint GetBuffer(string name)
        {
            if (Buffers.ContainsKey(name))
            {
                return Buffers[name];
            }
            else
            {
                return 0;
            }
        }

        public void SetInt(int location, int value)
        {
            GL.Uniform1(location, value);
        }

        public void SetFloat(int location, float value)
        {
            GL.Uniform1(location, value);
        }

        public void SetVector3(int location, Vector3 vector)
        {
            GL.Uniform3(location, vector);
        }

        public void SetBool(int location, bool value)
        {
            float toLoad = 0;

            if (value)
            {
                toLoad = 1;
            }
            GL.Uniform1(location, toLoad);
        }

        public void SetMatrix(int location, Matrix4 matrix)
        {
            GL.UniformMatrix4(location, false, ref matrix);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();
                GL.DetachShader(ProgramID, vertexShaderID);
                GL.DetachShader(ProgramID, fragmentShaderID);
                GL.DeleteShader(vertexShaderID);
                GL.DeleteShader(fragmentShaderID);
                GL.DeleteProgram(ProgramID);
            }
        }
    }
}
