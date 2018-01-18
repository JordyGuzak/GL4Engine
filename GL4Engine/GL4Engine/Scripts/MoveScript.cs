using GL4Engine.Core;
using OpenTK;
using OpenTK.Input;

namespace GL4Engine
{
    class MoveScript : Script
    {
        public float Speed { get; set; }

        public override void Start()
        {
            Speed = 10f;
        }

        public override void Update()
        {
            float speed = Speed;
            if (Input.GetKey(Key.LShift))
            {
                speed *= 2;
            }
            if (Input.GetKey(Key.W))
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey(Key.S))
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey(Key.D))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(Key.A))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(Key.Space))
            {
                transform.position += transform.up * speed * Time.deltaTime;
            }
            if (Input.GetKey(Key.LControl))
            {
                transform.position -= transform.up * speed * Time.deltaTime;
            }

        }
    }
}
