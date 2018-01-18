using GL4Engine.Core;
using OpenTK;

namespace GL4Engine
{
    class RotateScript : Script
    {
        float speed = 20f;
        float time = 0;

        public override void Start()
        {
            transform.rotation.Y = 0;
        }

        public override void Update()
        {
            time += Time.deltaTime;

            float angle = speed * Time.deltaTime;

            if (Input.GetKey(OpenTK.Input.Key.Right))
            {
                transform.Rotate(Vector3.UnitY, angle);
            }
        }
    }
}
