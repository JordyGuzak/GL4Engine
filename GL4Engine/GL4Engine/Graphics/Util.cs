using System;
using System.IO;

namespace GL4Engine.Graphics
{
    class Util
    {
        public static string ReadAllText(String path)
        {
            return File.ReadAllText(path);
        }
    }
}
