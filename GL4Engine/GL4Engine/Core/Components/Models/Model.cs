using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GL4Engine.Core
{
    abstract class Model : Component
    {
        public RawModel RawModel { get; private set; }

        public Model(RawModel rawModel)
        {
            RawModel = rawModel;
        }
    }
}
