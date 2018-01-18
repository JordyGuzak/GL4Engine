using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GL4Engine.Core
{
    class Time
    {
        public static float deltaTime = 0.0f;
        public static float elapsedTime = 0.0f;

        public static void UpdateTime(float time)
        {
            deltaTime = time;
            elapsedTime += time;
        }
    }
}
