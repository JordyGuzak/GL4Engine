using GL4Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GL4Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGame testGame = new TestGame();
            using (GL4Window game = new GL4Window(testGame, 800, 600, "GL4Engine"))
            {
                game.Run(1.0 / 60.0);
            }
        }
    }
}
