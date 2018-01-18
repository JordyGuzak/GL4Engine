using System;
using OpenTK.Graphics.OpenGL4;

namespace GL4Engine.Graphics
{
    class AttributeInfo
    {
        public String name = "";
        public int address = -1;
        public int size = 0;
        public ActiveAttribType type;
    }
}
