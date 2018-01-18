using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace GL4Engine.Graphics
{
    class Face
    {

        public Vector3[] Vertices { get; private set; }
        public Vector2[] TextureCoordinates { get; private set; }
        public Vector3[] Normals { get; private set; }
        public uint[] Indices { get; private set; }

    }
}
