{
    name = "Hologram Shader",

    options =
    {
        USE_COLOR = { true },
        SELECTION_MODE = { true },
    },

    properties =
    {
        _color = { "vec4", {1.0, 0.76393961906433, 0.0, 1.0} },
        _bands = { "float", 94.0 },
        _time = { "float", "0.0" },
    },

    pass =
    {
        base = "Surface",

        blendMode = "additive",
        depthWrite = false,
        depthFunc = "lessEqual",
        renderQueue = "transparent",
        colorMask = {"rgba"},
        cullFace = "back",

        vertex =
        [[
            out vec3 vPosition;
            void vertex(inout Vertex v, out Input o)
            {
                vPosition = position;
            }
        ]],

        surface =
        [[
            uniform vec4 _color;
            in vec3 vPosition;
            uniform float _bands;
            uniform float _time;
            
            float remap(float value, float minA, float maxA, float minB, float maxB)
            {
                return minB + (value - minA) * (maxB - minB) / (maxA - minA);
            }
            
            vec2 remap(vec2 value, vec2 minA, vec2 maxA, vec2 minB, vec2 maxB)
            {
                return minB + (value - minA) * (maxB - minB) / (maxA - minA);
            }
            
            vec3 remap(vec3 value, vec3 minA, vec3 maxA, vec3 minB, vec3 maxB)
            {
                return minB + (value - minA) * (maxB - minB) / (maxA - minA);
            }
            
            vec4 remap(vec4 value, vec4 minA, vec4 maxA, vec4 minB, vec4 maxB)
            {
                return minB + (value - minA) * (maxB - minB) / (maxA - minA);
            }
            
            vec2 remap(vec2 value, float minA, float maxA, float minB, float maxB)
            {
                return minB + (value - minA) * (maxB - minB) / (maxA - minA);
            }
            
            vec3 remap(vec3 value, float minA, float maxA, float minB, float maxB)
            {
                return minB + (value - minA) * (maxB - minB) / (maxA - minA);
            }
            
            vec4 remap(vec4 value, float minA, float maxA, float minB, float maxB)
            {
                return minB + (value - minA) * (maxB - minB) / (maxA - minA);
            }
            
            
            void surface(in Input IN, inout SurfaceOutput o)
            {
                o.emissive = 1.0;
                vec3 split_3 = vPosition;
                o.diffuse = (_color.rgb*vec3(((1.0 - dot(normalize(IN.viewPosition), normalize(IN.normal))) * remap(sin((split_3.g*_bands+(_time * 10.0))), vec2(-1.0, 1.0).x, vec2(-1.0, 1.0).y, vec2(0.0, 1.0).x, vec2(0.0, 1.0).y)), ((1.0 - dot(normalize(IN.viewPosition), normalize(IN.normal))) * remap(sin((split_3.g*_bands+(_time * 10.0))), vec2(-1.0, 1.0).x, vec2(-1.0, 1.0).y, vec2(0.0, 1.0).x, vec2(0.0, 1.0).y)), ((1.0 - dot(normalize(IN.viewPosition), normalize(IN.normal))) * remap(sin((split_3.g*_bands+(_time * 10.0))), vec2(-1.0, 1.0).x, vec2(-1.0, 1.0).y, vec2(0.0, 1.0).x, vec2(0.0, 1.0).y))));
                o.emission = vec3(0.0, 0.0, 0.0);
                o.opacity = 1.0;
            }
        ]]
    }
}
