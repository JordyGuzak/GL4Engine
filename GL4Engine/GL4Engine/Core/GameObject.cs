using System;
using System.Collections.Generic;

namespace GL4Engine.Core
{
    class GameObject : Object
    {
        public Transform transform;
        private List<Component> components;

        public GameObject(string name = null) : base(name)
        {
            transform = new Transform(this);
            components = new List<Component>();
            components.Add(transform);
        }

        /// <summary>
        /// Add component to GameObject.
        /// </summary>
        /// <param name="component"></param>
        public GameObject AddComponent(Component component)
        {
            // Abort when component is null
            if (!component) throw new ArgumentNullException("Component with value null is not allowed.");

            // Add component to list
            component.gameObject = this;
            component.transform = transform;
            components.Add(component);
            

            // Invoke OnScriptAdded event if the component is of type Script
            if (component is Script)
            {
                EventManager.Instance.OnScriptAdded(this, new ScriptAddedEventArgs() { Script = component as Script });
            }
            else if (component is Light)
            {
                EventManager.Instance.OnLightAdded(this, new LightAddedEventArgs() { Light = component as Light });
            }

            return this;
        }

        public GameObject RemoveComponent(Component component)
        {
            // Abort when component does not exist
            if (!components.Contains(component)) return this;

            // Remove component from list
            components.Remove(component);

            // Invoke OnScriptRemoved event if the component is of type Script
            if (component is Script)
            {
                EventManager.Instance.OnScriptRemoved(this, new ScriptRemovedEventArgs() { Script = component as Script });
            }
            else if (component is Light)
            {
                EventManager.Instance.OnLightRemoved(this, new LightRemovedEventArgs() { Light = component as Light });
            }

            return this;
        }

        /// <summary>
        /// Returns a List of all attached components.
        /// </summary>
        /// <returns></returns>
        public List<Component> GetComponents()
        {
            return components;
        }
    }
}
