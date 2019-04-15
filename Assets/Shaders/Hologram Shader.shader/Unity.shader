// Created with Shade for iPad
Shader "Shade/Hologram Shader"
{
    Properties
    {
        _color  ("Color", Color) = (1.0, 0.76393961906433, 0.0, 1.0)
        _bands  ("Bands", float) = 94.00
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
            float3 viewDirection;
            float3 objectPos;
            float3 normal;
            float4 color : COLOR;
        };

        float4 _color;

        float _bands;
        
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
            o.normal = COMPUTE_VIEW_NORMAL;
            o.viewDirection = -UnityObjectToViewPos(v.vertex.xyz);
            o.objectPos = v.vertex.xyz;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Emission = float3(0.0, 0.0, 0.0);
            float3 split_3 = IN.objectPos;
            o.Albedo = (_color.rgb*float3(((1.0 - dot(normalize(IN.viewDirection), normalize(IN.normal))) * remap(sin((split_3.g*_bands+(_Time.y * 10.0))), float2(-1.0, 1.0).x, float2(-1.0, 1.0).y, float2(0.0, 1.0).x, float2(0.0, 1.0).y)), ((1.0 - dot(normalize(IN.viewDirection), normalize(IN.normal))) * remap(sin((split_3.g*_bands+(_Time.y * 10.0))), float2(-1.0, 1.0).x, float2(-1.0, 1.0).y, float2(0.0, 1.0).x, float2(0.0, 1.0).y)), ((1.0 - dot(normalize(IN.viewDirection), normalize(IN.normal))) * remap(sin((split_3.g*_bands+(_Time.y * 10.0))), float2(-1.0, 1.0).x, float2(-1.0, 1.0).y, float2(0.0, 1.0).x, float2(0.0, 1.0).y))));
            o.Alpha = 1.0;
        }
        ENDCG
    }
}
