using GL4Engine.Core;
using OpenTK;
using System.IO.Ports;

namespace GL4Engine.Scripts
{
    class ArduinoSensor : Script
    {
        private SerialPort port;

        public override void Start()
        {
            port = new SerialPort("COM3", 115200);
            port.DtrEnable = true;
            port.DataReceived += OnDataReceived;
            port.Open();
        }

        public override void Update()
        {
            
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string line = port.ReadLine();
            string[] arr = line.Split('\t');
            if (arr[0] == "ypr")
            {
                float yaw = float.Parse(arr[1]);
                float pitch = float.Parse(arr[3]);
                float roll = float.Parse(arr[2]);


                yaw /= 100 * -1;
                pitch /= 100 * -1;
                roll /= 100 * -1;

                transform.rotation = Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(pitch), MathHelper.DegreesToRadians(yaw), MathHelper.DegreesToRadians(roll));
            }
        }

        public override void OnClose()
        {
            base.OnClose();
            port.Close();
        }
    }
}

