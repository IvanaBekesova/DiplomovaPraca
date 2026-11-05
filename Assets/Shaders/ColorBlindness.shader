Shader "Custom/ColorBlindness"
{
    Properties
    {
        [IntRange] _ColorBlindType ("Type (0 none, 1 protan, 2 deutan, 3 tritan)", Range(0, 3)) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "ColorBlindnessPass"
            ZTest Always
            ZWrite Off
            Cull Off
            Blend Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _ColorBlindType;

            half4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                half4 color = SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, input.texcoord, 0);
                int type = (int)round(_ColorBlindType);

                // matice podľa Machado et al. (2009)
                float3x3 m;
                if (type == 1)          // protanopia (chýbajú L čapíky – červená)
                {
                    m = float3x3( 0.152286,  1.052583, -0.204868,
                                  0.114503,  0.786281,  0.099216,
                                 -0.003882, -0.048116,  1.051998);
                }
                else if (type == 2)     // deuteranopia (chýbajú M čapíky – zelená)
                {
                    m = float3x3( 0.367322,  0.860646, -0.227968,
                                  0.280085,  0.672501,  0.047413,
                                 -0.011820,  0.042940,  0.968881);
                }
                else if (type == 3)     // tritanopia (chýbajú S čapíky – modrá)
                {
                    m = float3x3( 1.255528, -0.076749, -0.178779,
                                 -0.078411,  0.930809,  0.147602,
                                  0.004733,  0.691367,  0.303900);
                }
                else
                {
                    return color;       // 0 = bez efektu
                }

                color.rgb = saturate(mul(m, color.rgb));
                return color;
            }
            ENDHLSL
        }
    }
}