{
    name = "Depth Blending",

    options =
    {
        DOUBLE_SIDED = { true },
        USE_COLOR = { true },
        SELECTION_MODE = { true },
    },

    properties =
    {
        _time = { "float", "0.0" },
        _glowColor = { "vec4", {1.0, 0.61767345666885, 0.0, 1.0} },
        _blendDistance = { "float", 1.0 },
        _falloff = { "float", 4.0 },
        _glow = { "float", 5.0 },
    },

    pass =
    {
        base = "Surface",

        blendMode = "additive",
        depthWrite = false,
        depthFunc = "lessEqual",
        renderQueue = "transparent",
        colorMask = {"rgba"},
        cullFace = "none",

        vertex =
        [[
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

            

            
            void vertex(inout Vertex v, out Input o)
            {
                v.position += ((remap(sin((_time * 0.34868)), vec2(-1.0, 1.0).x, vec2(-1.0, 1.0).y, vec2(-0.25, 1.0).x, vec2(-0.25, 1.0).y) * (modelMatrix * vec4(v.normal, 0.0)).xyz)+vec3(0.0, 0.0, 0.0));
            }
        ]],

        surface =
        [[
            uniform vec4 _glowColor;
            uniform vec4 screenParams;
            uniform mat4 inverseProjectionMatrix;
            uniform vec4 depthParams;
            uniform highp sampler2D cameraDepthMap;
            uniform float _blendDistance;
            uniform float _falloff;
            uniform float _glow;
            float depthToLinear(float depth)
            {
                return ( depthParams.z + depthParams.w / depth );
            }
            
            float rawDepthToSceneDepth(vec2 screenUV, float sample)
            {
            #ifdef USE_LOGDEPTHBUF
                sample = depthToLinear(pow(depthParams.y + 1.0, sample) - 1.0);
            #endif
            
                vec4 sceneScreenPos = vec4(screenUV.x, screenUV.y, sample, 1.0) * 2.0 - 1.0;
                vec4 sceneViewPosition = inverseProjectionMatrix * sceneScreenPos;
                return -(sceneViewPosition.z / sceneViewPosition.w);
            }
            
            void surface(in Input IN, inout SurfaceOutput o)
            {
                o.emissive = 1.0;
                o.diffuse = vec3(0.11104, 0.091347, 0.14922);
                #ifdef USE_LOGDEPTHBUF
                float depth = rawDepthToSceneDepth((gl_FragCoord.xy * screenParams.zw), gl_FragDepth);
                #else
                float depth = rawDepthToSceneDepth((gl_FragCoord.xy * screenParams.zw), gl_FragCoord.z);
                #endif
                o.emission = (_glowColor.rgb * (pow((1.0 - clamp(((rawDepthToSceneDepth((gl_FragCoord.xy * screenParams.zw), texture(cameraDepthMap, (gl_FragCoord.xy * screenParams.zw)).r) - depth) / _blendDistance), 0.0, 1.0)), _falloff)*_glow));
                o.opacity = 1.0;
            }
        ]]
    }
}
