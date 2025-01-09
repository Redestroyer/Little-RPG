shader_type canvas_item;

// `uniform` variables are parameters that the material can receive from the engine.
uniform vec4 to : source_color;
uniform float lerp : hint_range(0.0, 1.0);

void fragment() {
	vec4 color = mix(COLOR, to, lerp);
	// We multiply the resulting colour's alpha by the input colour's alpha so the parts where the texture is invisible continue invisible.
	color.a *= COLOR.a;
	COLOR = color;
}
