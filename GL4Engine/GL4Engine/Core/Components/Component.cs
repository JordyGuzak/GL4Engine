using System.Collections.Generic;
using System.Linq;

namespace GL4Engine.Core
{
    abstract class Component : Object
    {
        public GameObject gameObject;
        public Transform transform;

        public Component() { }

        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.transform = gameObject.transform;
        }

        /// <summary>
        /// Get first component of type T in this GameObject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            return (T)gameObject.GetComponents().FirstOrDefault(x => x is T);
        }

        /// <summary>
        /// Get all components of type T in this GameObject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetComponents<T>() where T : Component
        {
            return gameObject.GetComponents().Where(x => x is T).Cast<T>().ToList();
        }

        /// <summary>
        /// Get component of type T in this GameObject or any of its children.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponentInChildren<T>() where T : Component
        {
            // Get component on this GameObject
            T component = GetComponent<T>();

            // Return if found something
            if (component) return component;

            // Else search in children
            foreach (Transform child in transform.GetChildren())
            {
                component = child.GetComponentInChildren<T>();
                if (component) break;
            }

            return component;
        }

        /// <summary>
        /// Get component of type T in this GameObject or any of its children.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetComponentsInChildren<T>() where T : Component
        {
            // A List that holds all components found of type T
            List<T> result = new List<T>();

            // Get component on this GameObject
            result = GetComponents<T>();

            // Do the same for all children
            foreach (Transform child in transform.GetChildren())
            {
                result.AddRange(GetComponentsInChildren<T>());
            }

            return result;
        }
    }
}
