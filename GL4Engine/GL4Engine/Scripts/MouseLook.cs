using GL4Engine.Core;
using OpenTK;
using System;

namespace GL4Engine.Scripts
{
    class MouseLook : Script
    {
        public float mouseX = 0f;
        public float mouseY = 0f;
        public float sensitivity = 7.5f;

        public override void Start()
        {
        }

        public override void Update()
        {
            mouseX = Input.mouseDeltaX * sensitivity * Time.deltaTime;
            mouseY = Input.mouseDeltaY * sensitivity * Time.deltaTime;

            //Clamp(ref mouseX, ref mouseY);

            transform.Rotate(transform.right, -mouseY);
            transform.Rotate(Vector3.UnitY, -mouseX);
        }

        public void Clamp(ref float x, ref float y)
        {
            if (x > 360) x = 0;
            if (x < -360) x = 0;
            if (y > 90) y = 90;
            if (y < -90) y = -90;
        }
    }
}
