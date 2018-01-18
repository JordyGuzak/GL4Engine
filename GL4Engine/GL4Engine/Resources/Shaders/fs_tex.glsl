#version 450

in vec3 v_position;
in vec2 f_textureCoord;
in vec3 v_normal;
in mat4 u_transformation;
in vec3 cameraPosition;

uniform sampler2D mainTexture;

uniform struct Material 
{
	vec3 diffuse;
	vec3 specular;
	float specular_exponent; //shininess
} material;

#define MAX_LIGHTS 10

uniform int numLights;

uniform struct Light
{
	int type;
	vec3 position;
	vec3 color;
	float attenuation;
	float ambientCoefficient;
	float coneAngle;
	vec3 coneDirection;

} lights[MAX_LIGHTS];

out vec4 final_color;


vec3 ApplyLight(Light light, vec3 surfaceColor, vec3 normal, vec3 surfacePosition, vec3 surfaceToCamera)
{
	vec3 surfaceToLight;
    float attenuation = 1.0;

    if(light.type == 0) 
	{
        //directional light
        surfaceToLight = normalize(light.position);
        attenuation = 1.0; //no attenuation for directional lights
    } 
	else 
	{
        //point light
        surfaceToLight = normalize(light.position - surfacePosition);
        float distanceToLight = length(light.position - surfacePosition);
        attenuation = 1.0 / (1.0 + light.attenuation * pow(distanceToLight, 2));

		//		cone restrictions (affects attenuation)
		// 1.	Get the direction for the center of the cone. The `normalize`
		//		function is called just in case `light.coneDirection` isn't
		//		already a unit vector.
		vec3 coneDirection = normalize(light.coneDirection);

		// 2.	Get the direction of the ray of light. This is the opposite
		//		of the direction from the surface to the light.
		vec3 rayDirection = -surfaceToLight;

		// 3.	Get the angle between the center of the cone and the ray of light.
		//		The combination of `acos` and `dot` return the angle in radians, then
		//		we convert it to degrees.
		float lightToSurfaceAngle = degrees(acos(dot(rayDirection, coneDirection)));

		// 4. Check if the angle is outside of the cone. If so, set the attenuation
		//    factor to zero, to make the light ray invisible.
		if (lightToSurfaceAngle > light.coneAngle)
		{
		  attenuation = 0.0;
		}
    }

	//ambient
    vec3 ambient = light.ambientCoefficient * surfaceColor.rgb * light.color;

    //diffuse
    float diffuseCoefficient = max(0.0, dot(normal, surfaceToLight));
    vec3 diffuse = diffuseCoefficient * surfaceColor.rgb * light.color;
    
    //specular
    float specularCoefficient = 0.0;
    if(diffuseCoefficient > 0.0)
        specularCoefficient = pow(max(0.0, dot(surfaceToCamera, reflect(-surfaceToLight, normal))), material.specular_exponent);
    vec3 specular = specularCoefficient * material.specular * light.color;

    //linear color (color before gamma correction)
    return ambient + attenuation * (diffuse + specular);
}

void main(void)
{
	vec3 normal = normalize(transpose(inverse(mat3(u_transformation))) * v_normal);
    vec3 surfacePosition = vec3(u_transformation * vec4(v_position, 1));
	vec4 surfaceColor = texture(mainTexture, f_textureCoord) * vec4(material.diffuse, 1);
	vec3 surfaceToCamera = normalize(cameraPosition - surfacePosition);

	vec3 linearColor = vec3(0);
	for (int i = 0; i < numLights; i++)
	{
		linearColor += ApplyLight(lights[i], surfaceColor.rgb, normal, surfacePosition, surfaceToCamera);
	}

	//final color (after gamma correction)
    vec3 gamma = vec3(1.0/2.2);
    final_color = vec4(pow(linearColor, gamma), surfaceColor.a);
}