Shader "Custom/TunnelVision"
{
    Properties
    {
        _TunnelSize ("Size", Range(0, 1)) = 0.3
        _TunnelSoftness ("Softness", Range(0.001, 0.5)) = 0.15
        _TunnelBlur ("Blur", Range(0, 1)) = 0
        _TunnelOpacity ("Opacity", Range(0, 1)) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "TunnelVisionPass"
            ZTest Always
            ZWrite Off
            Cull Off
            Blend Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            #define SAMPLES 32        // počet bodov na rozmazanie
            #define MAX_BLUR 0.05     // max. polomer rozmazania (časť výšky obrazu)

            float _TunnelSize;
            float _TunnelSoftness;
            float _TunnelBlur;
            float _TunnelOpacity;

            half4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float2 uv = input.texcoord;
                float aspect = _ScreenParams.x / _ScreenParams.y;

                // maska: 0 v strede (ostré videnie), 1 v okolí (postihnuté videnie)
                float2 centered = uv - 0.5;
                centered.x *= aspect;              // aby bol tunel kruh, nie elipsa
                float dist = length(centered);
                float mask = smoothstep(_TunnelSize, _TunnelSize + _TunnelSoftness, dist);

                half3 color = SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, uv, 0).rgb;

                // rozmazanie okolia
                float radius = mask * _TunnelBlur * MAX_BLUR;
                if (radius > 0.0005)
                {
                    half3 sum = 0;
                    for (int i = 0; i < SAMPLES; i++)
                    {
                        float r = sqrt((i + 0.5) / SAMPLES) * radius;
                        float theta = i * 2.39996323;
                        float2 offset = float2(cos(theta), sin(theta)) * r;
                        offset.x /= aspect;
                        sum += SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, uv + offset, 0).rgb;
                    }
                    color = sum / SAMPLES;
                }

                // stmavenie okolia
                color *= 1 - mask * _TunnelOpacity;

                return half4(color, 1);
            }
            ENDHLSL
        }
    }
}