Shader "Custom/CentralLoss"
{
    Properties
    {
        _ScotomaSize ("Size", Range(0, 0.7)) = 0.15
        _ScotomaSoftness ("Softness", Range(0.001, 0.5)) = 0.1
        _ScotomaBlur ("Blur", Range(0, 1)) = 1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "CentralLossPass"
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

            float _ScotomaSize;
            float _ScotomaSoftness;
            float _ScotomaBlur;

            half4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float2 uv = input.texcoord;
                float aspect = _ScreenParams.x / _ScreenParams.y;

                // maska: 1 v strede škvrny, 0 mimo nej
                float2 centered = uv - 0.5;
                centered.x *= aspect;              // aby bola škvrna kruh, nie elipsa
                float dist = length(centered);
                float mask = 1 - smoothstep(_ScotomaSize, _ScotomaSize + _ScotomaSoftness, dist);

                // polomer rozmazania pre tento pixel
                float radius = mask * _ScotomaBlur * MAX_BLUR;

                half4 original = SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, uv, 0);
                if (radius < 0.0005) return original;   // mimo škvrny – nič nerobíme

                // priemer bodov rozmiestnených v kruhu (špirála so zlatým uhlom)
                half3 sum = 0;
                for (int i = 0; i < SAMPLES; i++)
                {
                    float r = sqrt((i + 0.5) / SAMPLES) * radius;
                    float theta = i * 2.39996323;
                    float2 offset = float2(cos(theta), sin(theta)) * r;
                    offset.x /= aspect;
                    sum += SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, uv + offset, 0).rgb;
                }

                return half4(sum / SAMPLES, 1);
            }
            ENDHLSL
        }
    }
}