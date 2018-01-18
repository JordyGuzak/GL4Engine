using GL4Engine.Core;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace GL4Engine.Graphics
{
    class MasterRenderer : IDisposable
    {
        private Dictionary<Mesh, List<MeshRenderer>> meshRenderers = new Dictionary<Mesh, List<MeshRenderer>>();
        private List<Light> lights = new List<Light>();

        public MasterRenderer()
        {
            Init();
        }

        public void Init()
        {
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            //GL.Enable(EnableCap.FramebufferSrgb);
            //GL.Enable(EnableCap.Blend); GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrc1Alpha);
        }

        private void Prepare()
        {
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void Render(Camera camera)
        {
            Prepare();

            foreach (KeyValuePair<Mesh, List<MeshRenderer>> entry in meshRenderers)
            {
                Mesh mesh = entry.Key;

                mesh.Bind();

                foreach (MeshRenderer renderer in entry.Value)
                {
                    renderer.Draw(camera, lights.ToArray());
                }

                mesh.Unbind();
            }
        }

        public void ProcessMeshRenderer(MeshRenderer meshRenderer)
        {
            if (meshRenderers.ContainsKey(meshRenderer.Mesh))
            {
                meshRenderers[meshRenderer.Mesh].Add(meshRenderer);
            }
            else
            {
                List<MeshRenderer> mrList = new List<MeshRenderer>();
                mrList.Add(meshRenderer);
                meshRenderers.Add(meshRenderer.Mesh, mrList);
            }
        }

        public void ClearMeshRenderers()
        {
            meshRenderers.Clear();
        }

        public void AddLight(Light light)
        {
            lights.Add(light);
        }

        public void RemoveLight(Light light)
        {
            lights.Remove(light);
        }

        public void ClearLights()
        {
            lights.Clear();
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
                //shader.Dispose();
            }
        }
    }
}
