using System;
using OpenTK;

namespace GL4Engine.Core
{
    abstract class Object
    {
        public Guid id;
        public string name;

        public Object()
        {
            id = GenerateID();
            name = null;
        }

        public Object(string name)
        {
            this.name = name;
        }

        private Guid GenerateID()
        {
            return Guid.NewGuid();
        }

        public static GameObject Instantiate(Object original)
        {
            return Instantiate(original, null);
        }

        public static GameObject Instantiate(Object original, Transform parent)
        {
            return Instantiate(original, Vector3.Zero, Quaternion.Identity, parent);
        }

        public static GameObject Instantiate(Object original, Vector3 position, Quaternion rotation)
        {
            return Instantiate(original, position, rotation, null);
        }

        public static GameObject Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject gameObject = new GameObject();
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.transform.Parent = parent;
            return gameObject;
        }

        public static void Destroy(Object obj)
        {
            if (obj is GameObject)
            {
                GameObject gameObject = obj as GameObject;
                
                foreach(Transform transform in gameObject.transform.GetChildren())
                {
                    Destroy(transform.gameObject);
                }

                foreach(Component component in gameObject.GetComponents())
                {
                    Destroy(component);
                }
            }

            EventManager.Instance.OnDestroy(obj, new DestroyEventArgs() { Object = obj });
        }

        public static bool operator true(Object a)
        {
            return a != null;
        }

        public static bool operator false(Object a)
        {
            return a == null;
        }

        public static bool operator !(Object a)
        {
            return a == null;
        }

        //public static bool operator ==(Object a, Object b)
        //{
        //    return a == b;
        //}

        //public static bool operator !=(Object a, Object b)
        //{
        //    return a != b;
        //}
    }
}
