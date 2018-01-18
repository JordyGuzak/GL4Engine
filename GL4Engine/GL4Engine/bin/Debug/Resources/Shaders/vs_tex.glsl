#version 450

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 textureCoord;
layout(location = 2) in vec3 normal;

out vec3 v_position;
out vec2 f_textureCoord;
out vec3 v_normal;
out mat4 u_transformation;
out vec3 cameraPosition;

uniform mat4 transformation;
uniform mat4 view;
uniform mat4 projection;

uniform vec3 lightPosition;

void main()
{

	// Pass some variables to the fragment shader
	v_position = position;
    f_textureCoord = textureCoord;
    v_normal = normal;
	u_transformation = transformation;
	cameraPosition = vec3(view);

	// Apply all matrix transformations to vert
	gl_Position = projection * view *  transformation * vec4(position, 1.0);
}