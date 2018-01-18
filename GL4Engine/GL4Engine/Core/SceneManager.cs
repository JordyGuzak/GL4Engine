using System;
using System.Collections.Generic;
using System.Linq;

namespace GL4Engine.Core
{
    sealed class SceneManager
    {
        private static readonly SceneManager instance = new SceneManager();

        public static SceneManager Instance
        {
            get
            {
                return instance;
            }
        }

        public Scene ActiveScene { get { return activeScene; } }
        private Dictionary<string, Scene> scenes;
        private Scene activeScene;

        private SceneManager()
        {
            scenes = new Dictionary<string, Scene>();
            activeScene = null;
        }

        public void Update()
        {
            if (activeScene != null)
                activeScene.Update();
        }

        public void Render()
        {
            if (activeScene != null)
                activeScene.Render();
        }

        public void SetActiveScene(string name)
        {
            Scene scene;
            scenes.TryGetValue(name, out scene);

            if (activeScene != null)
                activeScene.Dispose();

            activeScene = scene ?? throw new KeyNotFoundException($"Scene with name '{name}' does not exist.");
            activeScene.Load();
        }

        public void AddScene(string name, Scene scene)
        {
            if (scene == null) throw new ArgumentNullException("Scene is null which is not accepted as a valid argument.");
            if (name == null) throw new ArgumentNullException("String 'name' is null which is not accepted as a valid argument.");
            scenes.Add(name, scene);
        }

        public void RemoveScene(string name)
        {
            scenes.Remove(name);
        }

        public void RemoveScene(Scene scene)
        {
            string key = scenes.FirstOrDefault(x => x.Value == scene).Key;
            if (key != null) RemoveScene(key);
        }
    }
}
