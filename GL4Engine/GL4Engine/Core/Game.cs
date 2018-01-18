
using GL4Engine.Graphics.Shaders;

namespace GL4Engine.Core
{
    abstract class Game
    {
        public Game()
        {
        }

        public void Update()
        {
            SceneManager.Instance.Update();
        }

        public void Render()
        {
            SceneManager.Instance.Render();
        }

        public abstract void OnLoad();
    }
}
