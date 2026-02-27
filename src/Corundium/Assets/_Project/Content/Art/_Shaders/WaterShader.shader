Shader "Custom/WaterShader"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.1, 0.5, 0.8, 0.9)
        _DeepColor ("Deep Water Color", Color) = (0.0, 0.2, 0.5, 1.0)
        _FresnelColor ("Fresnel Color", Color) = (0.7, 0.85, 1.0, 1.0)
        _Glossiness ("Smoothness", Range(0,1)) = 0.95
        _Metallic ("Metallic", Range(0,1)) = 0.0

        [Header(Animation)]
        _WaveSpeed ("Wave Speed", Range(0, 2)) = 0.5
        _WaveScale ("Wave Scale", Range(0, 10)) = 1.0
        _WaveHeight ("Wave Height", Range(0, 1)) = 0.3
        _NormalScale ("Normal Strength", Range(0, 2)) = 0.5

        [Header(Foam)]
        _FoamColor ("Foam Color", Color) = (1, 1, 1, 1)
        _FoamAmount ("Foam Amount", Range(0, 1)) = 0.3
        _FoamSpeed ("Foam Speed", Range(0, 5)) = 1.5
        _FoamScale ("Foam Scale", Range(0, 20)) = 5.0

        [Header(Fresnel)]
        _FresnelPower ("Fresnel Power", Range(0.1, 10)) = 3.0
        _FresnelIntensity ("Fresnel Intensity", Range(0, 1)) = 0.6

        [Header(Depth)]
        _DepthGradient ("Depth Gradient", Range(0, 5)) = 1.0
        _Transparency ("Transparency", Range(0, 1)) = 0.8
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "IgnoreProjector"="True"
        }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha:fade vertex:vert
        #pragma target 3.5

        struct Input
        {
            float3 worldPos;
            float3 viewDir;
        };

        half4 _Color;
        half4 _DeepColor;
        half4 _FresnelColor;
        half _Glossiness;
        half _Metallic;

        half _WaveSpeed;
        half _WaveScale;
        half _WaveHeight;
        half _NormalScale;

        half4 _FoamColor;
        half _FoamAmount;
        half _FoamSpeed;
        half _FoamScale;

        half _FresnelPower;
        half _FresnelIntensity;
        half _DepthGradient;
        half _Transparency;

        // Hash function
        float hash(float2 p)
        {
            float h = dot(p, float2(127.1, 311.7));
            return frac(sin(h) * 43758.5453123);
        }

        // Smooth noise
        float noise(float2 p)
        {
            float2 i = floor(p);
            float2 f = frac(p);
            f = f * f * (3.0 - 2.0 * f);

            float a = hash(i);
            float b = hash(i + float2(1.0, 0.0));
            float c = hash(i + float2(0.0, 1.0));
            float d = hash(i + float2(1.0, 1.0));

            return lerp(lerp(a, b, f.x), lerp(c, d, f.x), f.y);
        }

        void vert(inout appdata_full v)
        {
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
            float t = _Time.y * _WaveSpeed;

            // Animated waves
            float wave = 0.0;
            wave += sin(worldPos.x * _WaveScale + t * 2.0) * cos(worldPos.z * _WaveScale * 0.8 + t * 1.5);
            wave += sin(worldPos.x * _WaveScale * 1.4 - t * 1.8) * cos(worldPos.z * _WaveScale * 1.2 - t * 2.2) * 0.5;
            wave += sin(worldPos.x * _WaveScale * 0.6 + worldPos.z * _WaveScale * 0.7 + t * 1.3) * 0.3;

            v.vertex.y += wave * _WaveHeight;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float t = _Time.y;

            // Animated water normals
            float2 uv1 = IN.worldPos.xz * 0.1 + float2(t * 0.03, t * 0.02);
            float2 uv2 = IN.worldPos.xz * 0.15 + float2(-t * 0.04, t * 0.03);

            float n1 = noise(uv1);
            float n2 = noise(uv2);

            float3 normal;
            normal.x = (noise(uv1 + float2(0.01, 0)) - n1) * _NormalScale;
            normal.z = (noise(uv1 + float2(0, 0.01)) - n1) * _NormalScale;
            normal.y = 1.0;
            normal = normalize(normal);

            // Fresnel
            float fresnel = pow(1.0 - saturate(dot(normalize(IN.viewDir), normal)), _FresnelPower);

            // Foam
            float2 foamUV = IN.worldPos.xz * _FoamScale;
            float foam = 0.0;
            float amp = 0.5;
            for (int i = 0; i < 3; i++)
            {
                foam += noise(foamUV + t * _FoamSpeed * 0.3) * amp;
                foamUV *= 2.0;
                amp *= 0.5;
            }
            foam = pow(saturate(foam), 3.0) * _FoamAmount;

            // Depth color
            float depth = saturate(length(IN.viewDir) * _DepthGradient * 0.1);
            half4 waterColor = lerp(_Color, _DeepColor, depth);

            // Final color
            half3 col = waterColor.rgb;
            col = lerp(col, _FresnelColor.rgb, fresnel * _FresnelIntensity);
            col = lerp(col, _FoamColor.rgb, foam);

            o.Albedo = col;
            o.Normal = normal;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Transparency * lerp(0.75, 1.0, depth);
        }
        ENDCG
    }

    FallBack "Standard"
}
