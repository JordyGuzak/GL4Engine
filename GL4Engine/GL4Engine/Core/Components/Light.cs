using OpenTK;

public enum LightType
{
    DIRECTIONAL,
    POINT,
    SPOT
}

namespace GL4Engine.Core
{
    class Light : Component
    {
        public LightType Type { get; set; }
        public Vector3 Color { get; set; }
        public float Attenuation { get; set; }
        public float AmbientCoefficient { get; set; }
        public float ConeAngle { get; set; }
        public Vector3 ConeDirection { get; set; }

        public Light(LightType type, Vector3 color) : this(type, color, 0.2f, 0.01f, 0f, Vector3.Zero)
        {
        }

        public Light(LightType type, Vector3 color, float coneAngle, Vector3 coneDirection) : this (type, color, 0.2f, 0.01f, coneAngle, coneDirection)
        {
        }

        public Light(LightType type, Vector3 color, float attenuation, float ambientCoefficient, float coneAngle, Vector3 coneDirection)
        {
            Type = type;
            Color = color;
            Attenuation = attenuation;
            AmbientCoefficient = ambientCoefficient;
            ConeAngle = coneAngle;
            ConeDirection = coneDirection;
        }
    }
}
