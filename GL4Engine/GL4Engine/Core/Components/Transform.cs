using System;
using System.Collections.Generic;
using OpenTK;

namespace GL4Engine.Core
{
    public enum Space
    {
        Self,
        World,
    }

    class Transform : Component
    {
        private Transform parent;
        public Transform Parent { get { return parent; } set { SetParent(value); } }
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public int ChildCount { get { return childs.Count; } }
        private List<Transform> childs;

        public Vector3 forward
        {
            get
            {
                Matrix4 transformation = GetTransformationMatrix();
                return new Vector3(transformation[0,2], transformation[1, 2], transformation[2, 2]);
            }
        }

        public Vector3 right
        {
            get
            {
                Matrix4 transformation = GetTransformationMatrix();
                return new Vector3(transformation[0, 0], transformation[1, 0], transformation[2, 0]);
            }
        }
        public Vector3 up
        {
            get
            {
                Matrix4 transformation = GetTransformationMatrix();
                return new Vector3(transformation[0, 1], transformation[1, 1], transformation[2, 1]);
            }
        }

        public Matrix4 localToWorldMatrix
        {
            get
            {
                return GetTransformationMatrix();
            }
        }
        public Matrix4 worldToLocalMatrix
        {
            get
            {
                return Matrix4.Invert(localToWorldMatrix);
            }
        }

        public Transform(GameObject gameObject, Transform parent = null) : base(gameObject)
        {
            SetParent(parent);
            position = Vector3.Zero;
            rotation = Quaternion.Identity;
            scale = Vector3.One;
            childs = new List<Transform>();
            transform = this;
        }


        public Matrix4 GetTransformationMatrix()
        {
            return
                Matrix4.CreateScale(scale) *
                Matrix4.CreateFromQuaternion(rotation) *
                Matrix4.CreateTranslation(position);
        }

        /// <summary>
        /// Transforms position from local space to world space.
        /// </summary>
        /// <param name="local"></param>
        /// <returns></returns>
        public Vector3 TransformPoint(Vector3 local)
        {
            Vector3 world;
            var mat = localToWorldMatrix;
            Vector3.TransformPosition(ref local, ref mat, out world);
            return world;
        }


        /// <summary>
        /// Transforms position from world space to local space. The opposite of Transform.TransformPoint.
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        public Vector3 InverseTransformPoint(Vector3 world)
        {
            Vector3 local;
            var mat = worldToLocalMatrix;
            Vector3.TransformPosition(ref world, ref mat, out local);
            return local;
        }

        /// <summary>
        /// Rotate the transform around axis
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        public void Rotate(Vector3 axis, float angle)
        {
            transform.rotation *= Quaternion.FromAxisAngle(axis, MathHelper.DegreesToRadians(angle));
        }

        /// <summary>
        /// Adds a child Transform
        /// </summary>
        /// <param name="child"></param>
        private void AddChild(Transform child)
        {
            childs.Add(child);
        }

        /// <summary>
        /// Removes child Transform
        /// </summary>
        /// <param name="child"></param>
        private void RemoveChild(Transform child)
        {
            if (!childs.Contains(child)) return;
            childs.Remove(child);
        }

        /// <summary>
        /// Returns the Transform of child at given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Transform GetChild(int index)
        {
            return childs[index];
        }

        /// <summary>
        /// Returns the Transform of all children.
        /// </summary>
        /// <returns></returns>
        public List<Transform> GetChildren()
        {
            return childs;
        }

        /// <summary>
        /// This method is the same as the parent property except that it is possible to 
        /// make the Transform keep its local orientation rather than its global orientation. 
        /// This is managed by setting the worldPositionStays parameter to false. When SetParent 
        /// is called with only the single Transform argument the keepWorldPosition argument is set to true.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="keepWorldPosition"></param>
        public void SetParent(Transform parent, bool keepWorldPosition = true)
        {
            if (this == parent || this.parent == parent) return;

            if (this.parent != null)
            {
                this.parent.RemoveChild(this);
            }

            this.parent = parent;

            if (parent)
                parent.AddChild(this);
            // TODO keepWorldPosition
        }
    }
}
