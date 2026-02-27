Shader "Custom/WaterURP"
{
    Properties
    {
        // === Цвет воды ===
        _ShallowColor       ("Shallow Color",       Color) = (0.1, 0.6, 0.7, 0.8)
        _DeepColor          ("Deep Color",          Color) = (0.02, 0.15, 0.35, 1.0)
        _DepthMaxDistance   ("Depth Max Distance",  Float) = 5.0

        // === Пена ===
        _FoamColor          ("Foam Color",          Color) = (1, 1, 1, 1)
        _FoamDistance       ("Foam Distance",       Float) = 0.4
        _FoamNoiseTex       ("Foam Noise Texture",  2D) = "white" {}
        _FoamNoiseScale     ("Foam Noise Scale",    Float) = 3.0
        _FoamNoiseSpeed     ("Foam Noise Speed",    Float) = 0.3

        // === Нормалмапы ===
        _NormalMap          ("Normal Map A",        2D) = "bump" {}
        _NormalMapB         ("Normal Map B",        2D) = "bump" {}
        _NormalStrength     ("Normal Strength",     Range(0,3)) = 1.0
        _NormalSpeed        ("Normal Scroll Speed", Vector) = (0.05, 0.03, -0.04, 0.02)
        _NormalScale        ("Normal Scale",        Float) = 1.5

        // === Волны Герстнера ===
        _WaveA              ("Wave A (dirXY, steep, wavelength)", Vector) = (1.0,  0.2,  0.35, 17.3)
        _WaveB              ("Wave B (dirXY, steep, wavelength)", Vector) = (0.8,  0.6,  0.25, 11.7)
        _WaveC              ("Wave C (dirXY, steep, wavelength)", Vector) = (-0.3, 1.0,  0.18,  6.83)
        _WaveD              ("Wave D (dirXY, steep, wavelength)", Vector) = (0.5, -0.85, 0.12,  4.19)
        _WaveSpeed          ("Wave Speed",          Float) = 1.0

        // === Шум фазы волн ===
        // Шум подмешивается прямо в фазу f каждой волны Герстнера —
        // это физически ломает гребни, а не просто добавляет "рябь сверху"
        _PhaseNoiseScale    ("Phase Noise Scale",   Float) = 0.06     // масштаб шума в пространстве
        _PhaseNoiseStrength ("Phase Noise Strength",Range(0, 8)) = 2.5 // сила искажения фазы (рад)
        _PhaseNoiseSpeed    ("Phase Noise Speed",   Float) = 0.04     // скорость дрейфа шума

        // === Отражение/преломление ===
        _ReflectionStrength ("Reflection Strength", Range(0,1)) = 0.5
        _RefractionStrength ("Refraction Strength", Range(0,0.1)) = 0.02
        _Smoothness         ("Smoothness",          Range(0,1)) = 0.92

        // === Fresnel ===
        _FresnelPower       ("Fresnel Power",       Range(0.1, 10)) = 4.0

        // === SSS ===
        _SSSColor           ("SSS Color",           Color) = (0.0, 0.8, 0.5, 1)
        _SSSStrength        ("SSS Strength",        Range(0,2)) = 0.6
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType"     = "Transparent"
            "Queue"          = "Transparent"
        }

        Pass
        {
            Name "WaterForward"
            Tags { "LightMode" = "UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _ShallowColor;
                float4 _DeepColor;
                float  _DepthMaxDistance;

                float4 _FoamColor;
                float  _FoamDistance;
                float  _FoamNoiseScale;
                float  _FoamNoiseSpeed;

                float  _NormalStrength;
                float4 _NormalSpeed;
                float  _NormalScale;

                float4 _WaveA, _WaveB, _WaveC, _WaveD;
                float  _WaveSpeed;

                float  _PhaseNoiseScale;
                float  _PhaseNoiseStrength;
                float  _PhaseNoiseSpeed;

                float  _ReflectionStrength;
                float  _RefractionStrength;
                float  _Smoothness;
                float  _FresnelPower;

                float4 _SSSColor;
                float  _SSSStrength;
            CBUFFER_END

            TEXTURE2D(_NormalMap);    SAMPLER(sampler_NormalMap);
            TEXTURE2D(_NormalMapB);   SAMPLER(sampler_NormalMapB);
            TEXTURE2D(_FoamNoiseTex); SAMPLER(sampler_FoamNoiseTex);

            // ============================================================
            // ПРОЦЕДУРНЫЙ ШУМ
            // Gradient noise (Perlin-style) + FBM с поворотом октав
            // Работает в мировых координатах — никогда не тайлится
            // ============================================================
            float2 Hash2(float2 p)
            {
                p = float2(dot(p, float2(127.1, 311.7)),
                           dot(p, float2(269.5, 183.3)));
                return -1.0 + 2.0 * frac(sin(p) * 43758.5453123);
            }

            float GradNoise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                float2 u = f * f * (3.0 - 2.0 * f);
                return lerp(
                    lerp(dot(Hash2(i + float2(0,0)), f - float2(0,0)),
                         dot(Hash2(i + float2(1,0)), f - float2(1,0)), u.x),
                    lerp(dot(Hash2(i + float2(0,1)), f - float2(0,1)),
                         dot(Hash2(i + float2(1,1)), f - float2(1,1)), u.x), u.y);
            }

            // FBM — 4 октавы, матрица поворота 37° между ними
            // Поворот гарантирует, что октавы не совпадают по осям → нет артефактов
            float FBM4(float2 p)
            {
                const float2x2 ROT = float2x2(0.80, -0.60, 0.60, 0.80);
                float v = 0.0, a = 0.5, f = 1.0;
                UNITY_UNROLL
                for (int i = 0; i < 4; i++)
                {
                    v += a * GradNoise(p * f);
                    p  = mul(ROT, p);
                    f *= 2.13;  // не кратно 2 — разные пространственные частоты
                    a *= 0.48;
                }
                return v; // диапазон примерно [-1, 1]
            }

            // Двумерный FBM — возвращает два независимых значения шума
            // Используется для искажения фазы по двум осям
            float2 FBM4_2(float2 p)
            {
                return float2(
                    FBM4(p),
                    FBM4(p + float2(3.7, 8.3)) // сдвиг — другое "случайное поле"
                );
            }

            // ============================================================
            // ВОЛНА ГЕРСТНЕРА С ИСКАЖЕНИЕМ ФАЗЫ
            //
            // Ключевая идея: фаза f = k*(dot(d, p) - c*t) + phaseOffset + noisePhase
            // noisePhase — шум, вычисленный в пространстве волны.
            // Это физически изменяет скорость распространения гребня в каждой точке,
            // т.е. гребни буквально изламываются и никогда не повторяются.
            // ============================================================
            void GerstnerWaveNoisy(
                float4  wave,
                float   basePhase,       // постоянный фазовый сдвиг волны
                float   noisePhase,      // шумовое искажение фазы (в радианах)
                float3  posWS,
                inout float3 displacement,
                inout float3 tangent,
                inout float3 binormal)
            {
                float  steep      = wave.z;
                float  wavelength = wave.w;
                float  k          = TWO_PI / wavelength;
                float  c          = sqrt(9.8 / k);
                float2 d          = normalize(wave.xy);

                // Фаза с подмешанным шумом — вот где магия
                float  f = k * (dot(d, posWS.xz) - c * _Time.y * _WaveSpeed)
                           + basePhase
                           + noisePhase; // <-- шум искажает фазу напрямую

                float  a = steep / k;
                float  sinF = sin(f), cosF = cos(f);

                displacement += float3(d.x * (a * cosF),
                                            a *      sinF,
                                       d.y * (a * cosF));

                tangent  += float3(-d.x * d.x * steep * sinF,
                                    d.x *        steep * cosF,
                                   -d.x * d.y * steep * sinF);
                binormal += float3(-d.x * d.y * steep * sinF,
                                    d.y *        steep * cosF,
                                   -d.y * d.y * steep * sinF);
            }

            // ============================================================
            // Структуры
            // ============================================================
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float2 uv         : TEXCOORD1;
                float4 screenPos  : TEXCOORD2;
                float3 normalWS   : TEXCOORD3;
                float3 viewDirWS  : TEXCOORD4;
                float  fogCoord   : TEXCOORD5;
            };

            // ============================================================
            // ВЕРШИННЫЙ ШЕЙДЕР
            // ============================================================
            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                float3 posWS    = TransformObjectToWorld(IN.positionOS.xyz);
                float3 tangent  = float3(1, 0, 0);
                float3 binormal = float3(0, 0, 1);
                float3 disp     = float3(0, 0, 0);

                // ----------------------------------------------------------
                // Шум фазы: вычисляем поле искажений в мировом пространстве.
                // Поле медленно дрейфует со временем (_PhaseNoiseSpeed) —
                // паттерн меняется плавно, а не "застывает".
                // ----------------------------------------------------------
                float2 noiseCoord = posWS.xz * _PhaseNoiseScale
                                    + _Time.y * _PhaseNoiseSpeed;

                // Два независимых поля для горизонтальных волн
                float2 noiseVec = FBM4_2(noiseCoord); // [-1,1] каждое

                // Разные масштабы шума для каждой волны —
                // каждая волна искажается в своём пространственном масштабе
                float phaseA = FBM4(noiseCoord * 1.00) * _PhaseNoiseStrength;
                float phaseB = FBM4(noiseCoord * 1.31 + float2(1.7, 4.1)) * _PhaseNoiseStrength;
                float phaseC = FBM4(noiseCoord * 1.73 + float2(8.3, 2.9)) * _PhaseNoiseStrength;
                float phaseD = FBM4(noiseCoord * 2.17 + float2(3.1, 7.5)) * _PhaseNoiseStrength;

                // Четыре волны Герстнера с фазовым шумом
                // Базовые фазы — иррациональные числа, исключают кратность
                GerstnerWaveNoisy(_WaveA, 0.0000, phaseA, posWS, disp, tangent, binormal);
                GerstnerWaveNoisy(_WaveB, 1.7321, phaseB, posWS, disp, tangent, binormal); // √3
                GerstnerWaveNoisy(_WaveC, 3.1415, phaseC, posWS, disp, tangent, binormal); // π
                GerstnerWaveNoisy(_WaveD, 4.6692, phaseD, posWS, disp, tangent, binormal); // Фейгенбаум

                // ----------------------------------------------------------
                // Дополнительное вертикальное смещение от шума —
                // добавляет мелкую рябь поверх крупных волн
                // ----------------------------------------------------------
                float2 rippleCoord = posWS.xz * _PhaseNoiseScale * 3.5
                                     + _Time.y * _PhaseNoiseSpeed * 2.1;
                float ripple = FBM4(rippleCoord) * _PhaseNoiseStrength * 0.15;
                disp.y += ripple;

                // Вклад мелкой ряби в нормаль
                float eps  = 0.2;
                float dRdx = FBM4(rippleCoord + float2(eps,0)) - FBM4(rippleCoord - float2(eps,0));
                float dRdz = FBM4(rippleCoord + float2(0,eps)) - FBM4(rippleCoord - float2(0,eps));
                float rFactor = _PhaseNoiseStrength * 0.15 / (2.0 * eps);
                tangent  += float3(0, dRdx * rFactor, 0);
                binormal += float3(0, dRdz * rFactor, 0);

                posWS += disp;

                float3 normalWS = normalize(cross(binormal, tangent));

                OUT.positionWS = posWS;
                OUT.positionCS = TransformWorldToHClip(posWS);
                OUT.uv         = IN.uv;
                OUT.screenPos  = ComputeScreenPos(OUT.positionCS);
                OUT.normalWS   = normalWS;
                OUT.viewDirWS  = GetWorldSpaceViewDir(posWS);
                OUT.fogCoord   = ComputeFogFactor(OUT.positionCS.z);

                return OUT;
            }

            // ============================================================
            // ФРАГМЕНТНЫЙ ШЕЙДЕР
            // ============================================================
            half4 frag(Varyings IN) : SV_Target
            {
                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

                // Глубина
                float sceneDepth01     = SampleSceneDepth(screenUV);
                float sceneDepthLinear = LinearEyeDepth(sceneDepth01, _ZBufferParams);
                float waterDepth       = saturate((sceneDepthLinear - IN.screenPos.w) / _DepthMaxDistance);

                // Нормалмапы привязаны к мировым координатам (не UV меша)
                float2 wsUV = IN.positionWS.xz * 0.1;
                float2 uvA  = wsUV * _NormalScale       + _NormalSpeed.xy * _Time.y;
                float2 uvB  = wsUV * _NormalScale * 1.4 + _NormalSpeed.zw * _Time.y;
                float2 uvC  = wsUV * _NormalScale * 0.65
                              + float2(_NormalSpeed.y, -_NormalSpeed.x) * _Time.y * 0.6;

                float3 nA = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap,  sampler_NormalMap,  uvA), _NormalStrength);
                float3 nB = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMapB, sampler_NormalMapB, uvB), _NormalStrength);
                float3 nC = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap,  sampler_NormalMap,  uvC), _NormalStrength * 0.5);
                float3 bump = normalize(nA + nB + nC);

                float3 N = NormalizeNormalPerPixel(
                    float3(IN.normalWS.x + bump.x,
                           IN.normalWS.y,
                           IN.normalWS.z + bump.y));
                float3 V = normalize(IN.viewDirWS);

                float fresnel = pow(1.0 - saturate(dot(N, V)), _FresnelPower);

                // Преломление
                half4 refrColor = half4(
                    SampleSceneColor(saturate(screenUV + bump.xy * _RefractionStrength)), 1.0);

                // Цвет воды
                half4 waterColor = lerp(_ShallowColor, _DeepColor, waterDepth);

                // SSS
                float3 lightDir = normalize(_MainLightPosition.xyz);
                float  sssWrap  = saturate(dot(-V, lightDir) * 0.5 + 0.5);
                half3  sssColor = _SSSColor.rgb * pow(sssWrap, 3.0) * _SSSStrength;

                // Освещение
                Light  mainLight = GetMainLight(TransformWorldToShadowCoord(IN.positionWS));
                float3 H    = normalize(V + lightDir);
                float  spec = pow(max(dot(N, H), 0.0), 512.0 * _Smoothness) * _Smoothness;
                float  NdotL = max(dot(N, lightDir), 0.0);
                half3  diff  = mainLight.color * mainLight.shadowAttenuation * NdotL;

                // Пена в мировых координатах
                float2 foamUV   = IN.positionWS.xz * _FoamNoiseScale * 0.1
                                  + _Time.y * _FoamNoiseSpeed;
                float foamMask  = saturate(1.0 - waterDepth / max(_FoamDistance, 0.001));
                float foamNoise = SAMPLE_TEXTURE2D(_FoamNoiseTex, sampler_FoamNoiseTex, foamUV).r;
                float foam      = step(1.0 - foamMask, foamNoise) * foamMask;

                // Финальный цвет
                half3 col = waterColor.rgb;
                col = lerp(refrColor.rgb, col, waterColor.a * waterDepth);
                col += col * diff * 0.4 + sssColor;
                col += mainLight.color * spec;
                col  = lerp(col, _FoamColor.rgb, foam);
                col  = lerp(col, half3(0.5, 0.7, 1.0), fresnel * _ReflectionStrength);

                float alpha = lerp(_ShallowColor.a, _DeepColor.a, waterDepth);
                alpha = max(alpha, fresnel * 0.8);
                alpha = max(alpha, foam);

                col = MixFog(col, IN.fogCoord);
                return half4(col, alpha);
            }
            ENDHLSL
        }
    }
}
