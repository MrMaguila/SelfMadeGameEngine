#version 330 core

out vec4 fragColor;
in vec2 fragTexCoord;

in float intensity;
uniform sampler2D image;
//uniform vec2 screenSize;

float offsets[3] = float[](0.0, 1.3846153846, 3.2307692308);
uniform float weight[3] = float[] (0.2270270270, 0.3945945946, 0.1216216216);

void main()
{
	vec2 texelSize = 1.0 / textureSize(image, 0);	
	//vec2 fragTexCoord = vec2(gl_FragCoord.x / screenSize.x, gl_FragCoord.y / screenSize.y);
	vec3 result = texture(image, fragTexCoord).rgb * weight[0]; //contribution of current fragment
	
	for(int i = 1; i < 3; ++i)
	{
		float w = weight[i];
		float offset = offsets[i] * (fragTexCoord.y < 0.7 ? 0.0 : intensity);
		result += texture(image, fragTexCoord + vec2(texelSize.x * offset, 0.0)).rgb * w;
		result += texture(image, fragTexCoord - vec2(texelSize.x * offset, 0.0)).rgb * w;
		result += texture(image, fragTexCoord + vec2(0.0, texelSize.y * offset)).rgb * w;
		result += texture(image, fragTexCoord - vec2(0.0, texelSize.y * offset)).rgb * w;
	}
	
	fragColor = vec4(result / 2, 1.0f);
}

