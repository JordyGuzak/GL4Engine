using GL4Engine.Graphics;
using System;
using System.Collections.Generic;

namespace GL4Engine.Core
{
    class Scene : IDisposable
    {
        private MasterRenderer renderer;
        private List<GameObject> rootGameObjects;
        private List<Object> objectsToDestroy;
        private List<Script> scripts;
        private Camera mainCamera;

        public Scene()
        {
            rootGameObjects = new List<GameObject>();
            objectsToDestroy = new List<Object>();
            scripts = new List<Script>();
            SubscribeEvents();
        }

        public void Load()
        {
            mainCamera = new Camera();
            Camera.mainCamera = mainCamera;
            Add(new GameObject("Main Camera").AddComponent(mainCamera));
            renderer = new MasterRenderer();
        }

        public void Update()
        {
            // Update Scripts
            foreach (Script script in scripts)
            {
                script.Update();
            }

            // Update Mesh Renderers
            renderer.ClearMeshRenderers();

            foreach (GameObject go in rootGameObjects)
            {
                foreach (MeshRenderer mr in go.transform.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.ProcessMeshRenderer(mr);
                }
            }

            // Destroy GameObjects at the end of update cycle
            DestroyObjects();
        }

        public void Render()
        {
            renderer.Render(mainCamera);
        }

        public Scene Add(GameObject gameObject)
        {
            rootGameObjects.Add(gameObject);
            return this;
        }

        public Scene Remove(GameObject gameObject)
        {
            if (rootGameObjects.Contains(gameObject))
                rootGameObjects.Remove(gameObject);

            return this;
        }

        public GameObject Find(String name)
        {
            return rootGameObjects.Find(g => g.name.Equals(name));
        }

        private void DestroyObjects()
        {
            foreach(Object obj in objectsToDestroy)
            {
                if (obj is GameObject)
                {
                    GameObject gameObject = obj as GameObject;

                    // If it has a parent, remove this gameObject from it children
                    if (gameObject.transform.Parent != null)
                    {
                        gameObject.transform.Parent.GetChildren().Remove(gameObject.transform);
                    }
                }
                else if (obj is Component)
                {
                    // Remove component from its GameObject's list of components
                    Component component = obj as Component;
                    component.gameObject.RemoveComponent(component);
                }
            }
        }

        private void SubscribeEvents()
        {
            EventManager.ScriptAdded += AddScript;
            EventManager.ScriptRemoved += RemoveScript;
            EventManager.Destroy += AddToDestroy;
            EventManager.LightAdded += AddLight;
            EventManager.LightRemoved += RemoveLight;
        }

        private void UnsubscribeEvents()
        {
            EventManager.ScriptAdded -= AddScript;
            EventManager.ScriptRemoved -= RemoveScript;
            EventManager.Destroy -= AddToDestroy;
            EventManager.LightAdded -= AddLight;
            EventManager.LightRemoved -= RemoveLight;
        }

        private void AddLight(object sender, LightAddedEventArgs e)
        {
            renderer.AddLight(e.Light);
        }

        private void RemoveLight(object sender, LightRemovedEventArgs e)
        {
            renderer.RemoveLight(e.Light);
        }

        private void AddToDestroy(object sender, DestroyEventArgs e)
        {
            objectsToDestroy.Add(e.Object);
        }

        private void AddScript(object sender, ScriptAddedEventArgs e)
        {
            e.Script.Start();
            scripts.Add(e.Script);
        }

        private void RemoveScript(object sender, ScriptRemovedEventArgs e)
        {
            scripts.Remove(e.Script);
        }

        private void Clear()
        {
            scripts.Clear();
            objectsToDestroy.Clear();
            rootGameObjects.Clear();
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
                UnsubscribeEvents();
                Clear();
            }
        }
    }
}
