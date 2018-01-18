#version 450

in vec2 out_textureCoord;
in vec3 surfaceNormal;

out vec4 out_color;

uniform vec3 diffuseColor;
uniform vec3 ambientLight;
uniform sampler2D mainTexture;

struct BaseLight
{
	vec3 color;
	float intensity;
};

struct DirectionalLight
{
	BaseLight base;
	vec4 direction;
};

vec4 calculateLight(BaseLight base, vec4 direction, vec3 normal)
{
	float diffuseFactor = dot(normal, -direction);

	vec4 diffuseColor = vec4(0, 0, 0, 0);

	if (diffuseFactor > 0)
	{
		diffuseColor = vec4(base.color, 1.0) * base.intensity * diffuseFactor;
	}

	return diffuseColor;
}

vec4 calculateDirectionalLight(DirectionalLight directionalLight, vec3 normal)
{
	return calculateLight(directionalLight.base, -directionalLight.direction, normal);
}

void main(void)
{
	vec4 unitNormal = normalize(surfaceNormal);

	vec4 totalLight = vec4(ambientLight, 1);
	vec4 color = vec4(diffuseColor, 1);
	vec4 textureColor = texture(mainTexture, out_textureCoord);

	if (textureColor != vec4(0, 0, 0, 0))
	{
		color *= textureColor;
	} 

	out_color = color * totalLight;
}