// Created with Shade for iPad
Shader "Shade/Depth Blending"
{
    Properties
    {
        _glowColor  ("Glow Color", Color) = (1.0, 0.61767345666885, 0.0, 1.0)
        _blendDistance  ("Blend Distance", Range (0.00, 1.00)) = 1.00
        _falloff  ("Falloff", Range (0.00, 1.00)) = 4.00
        _glow  ("Glow", Range (0.00, 1.00)) = 5.00
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend One One
        ZWrite Off
        LOD 200

        CGPROGRAM

        #pragma target 4.0

        // Unlit model
        #pragma surface surf NoLighting vertex:vert noforwardadd addshadow

        fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            fixed4 c;
            c.rgb = s.Albedo + s.Emission.rgb;
            c.a = s.Alpha;
            return c;
        }

        struct Input {
            float2 texcoord : TEXCOORD0;
            float4 screenPos : TEXCOORD1;
            float eyeDepth;
            float3 worldNormal; 
            float4 color : COLOR;
        };

        float4 _glowColor;

        float _blendDistance;

        float _falloff;

        float _glow;

        sampler2D_float _CameraDepthTexture;
        float4 _CameraDepthTexture_TexelSize;

        
        float remap(float value, float minA, float maxA, float minB, float maxB)
        {
            return minB + (value - minA) * (maxB - minB) / (maxA - minA);
        }
        
        float2 remap(float2 value, float2 minA, float2 maxA, float2 minB, float2 maxB)
        {
            return minB + (value - minA) * (maxB - minB) / (maxA - minA);
        }
        
        float3 remap(float3 value, float3 minA, float3 maxA, float3 minB, float3 maxB)
        {
            return minB + (value - minA) * (maxB - minB) / (maxA - minA);
        }
        
        float4 remap(float4 value, float4 minA, float4 maxA, float4 minB, float4 maxB)
        {
            return minB + (value - minA) * (maxB - minB) / (maxA - minA);
        }
        
        float2 remap(float2 value, float minA, float maxA, float minB, float maxB)
        {
            return minB + (value - minA) * (maxB - minB) / (maxA - minA);
        }
        
        float3 remap(float3 value, float minA, float maxA, float minB, float maxB)
        {
            return minB + (value - minA) * (maxB - minB) / (maxA - minA);
        }
        
        float4 remap(float4 value, float minA, float maxA, float minB, float maxB)
        {
            return minB + (value - minA) * (maxB - minB) / (maxA - minA);
        }
        
        
        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.texcoord = v.texcoord;
            COMPUTE_EYEDEPTH(o.eyeDepth);
            o.screenPos = ComputeGrabScreenPos(UnityObjectToClipPos(v.vertex));
            v.vertex.xyz += ((remap(sin((_Time.y * 0.34868)), float2(-1.0, 1.0).x, float2(-1.0, 1.0).y, float2(-0.25, 1.0).x, float2(-0.25, 1.0).y) * normalize( mul( float4( v.normal, 0.0 ), unity_ObjectToWorld ).xyz ))+float3(0.0, 0.0, 0.0));
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Emission = (_glowColor.rgb * (pow((1.0 - clamp(((LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, IN.screenPos.xy/IN.screenPos.w)) - IN.eyeDepth) / _blendDistance), 0.0, 1.0)), _falloff)*_glow));
            o.Albedo = float3(0.11104, 0.091347, 0.14922);
            o.Alpha = 1.0;
        }
        ENDCG
    }
}
