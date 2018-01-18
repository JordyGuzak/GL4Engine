using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;

namespace GL4Engine.Core
{
    class GL4Window : GameWindow
    {
        public static int WIDTH = 800;
        public static int HEIGHT = 600;

        private Game game;

        public GL4Window(Game game, int width = 800, int height = 600, string title = "GL4Engine") : base
            (
                  width, height,
                  OpenTK.Graphics.GraphicsMode.Default,
                  title,
                  GameWindowFlags.Default,
                  DisplayDevice.Default,
                  //Major Minor implicitly assigned to 4.0
                  //It's best to set to your version of GL
                  //so look at the method below for help.
                  //**do not set to a version above your own
                  4, //OpenGL major version
                  0, // OpenGL minor version
                     //Make sure that we are only using 4.0 related stuff.
                  OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible
            )
        {
            this.game = game;
            Title += " : OpenGL Version: " + GL.GetString(StringName.Version);
            WIDTH = width;
            HEIGHT = height;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            game.OnLoad();
            #region GL_VERSION
            //this will return your version of opengl
            int major, minor;
            GL.GetInteger(GetPName.MajorVersion, out major);
            GL.GetInteger(GetPName.MinorVersion, out minor);
            Console.WriteLine("Major {0}\nMinor {1}", major, minor);
            //you can also get your GLSL version, although not sure if it varies from the above
            Console.WriteLine("GLSL {0}", GL.GetString(StringName.ShadingLanguageVersion));
            #endregion
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Time.UpdateTime((float)e.Time);
            Input.UpdateCurrentState();
            Input.UpdateCursorState();
            Input.UpdateMouseDelta();
            game.Update();
            Input.UpdatePreviousState();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            game.Render();
            GL.Flush();
            SwapBuffers();
        }
    }
}
