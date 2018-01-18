#version 450

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 textureCoord;
layout(location = 2) in vec3 normal;

out vec2 out_textureCoord;
out vec3 surfaceNormal;

uniform mat4 transformation;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	vec4 worldPosition = transformation * vec4(position, 1.0);
	gl_Position = projection * view * worldPosition;
	out_textureCoord = textureCoord;
	surfaceNormal = (transformation * vec4(normal, 0, 0)).xyz;
}