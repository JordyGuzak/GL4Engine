using System;
using OpenTK;

namespace GL4Engine.Core
{
    class Camera : Behaviour
    {
        public static Camera mainCamera;

        public float FOV { get; set; }
        public float AspectRatio { get; set; }
        public float NearPlane { get; set; }
        public float FarPlane { get; set; }
        public Matrix4 ProjectionMatrix { get; set; }

        public Camera() : this(60, 1.3333f, 0.01f, 4000f)
        {
        }

        public Camera(float fov, float aspectRatio) : this(fov, aspectRatio, 0.01f, 4000f)
        {
        }

        public Camera(float fov, float aspectRatio, float nearPlane, float farPlane)
        {
            FOV = fov;
            AspectRatio = aspectRatio;
            NearPlane = nearPlane;
            FarPlane = farPlane;
            ProjectionMatrix = CreateProjectionMatrix();
        }

        private Matrix4 CreateProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(FOV * (float)(Math.PI / 180f), AspectRatio, NearPlane, FarPlane);
        }

        public Matrix4 GetViewMatrix()
        {
            Matrix4 rotation = Matrix4.CreateFromQuaternion(transform.rotation);
            Matrix4 translation = Matrix4.CreateTranslation(-transform.position);
            return translation * rotation;
            //return Matrix4.LookAt(-transform.position, -Vector3.UnitZ, Vector3.UnitY);
        }
    }
}
