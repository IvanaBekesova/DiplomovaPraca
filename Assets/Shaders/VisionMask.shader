Shader "Custom/VisionMask"
{
    Properties
    {
        _Angle ("Angle", Float) = 0
        _Percent ("Percent", Float) = 0
        _Softness ("Softness", Float) = 0.1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "VisionMaskPass"
            ZTest Always
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _Angle;
            float _Percent;
            float _Softness;

            half4 Frag(Varyings input) : SV_Target
            {
                float2 uv = input.texcoord - 0.5;
                float rad = _Angle * 3.14159265 / 180.0;
                float2 dir = float2(cos(rad), sin(rad));
                float proj = dot(uv, dir);
                float soft = max(_Softness * 0.1, 0.001);
                float reach = 0.5 * (abs(dir.x) + abs(dir.y));        
                float edge = lerp(-reach - soft, reach + soft, _Percent);
                float alpha = smoothstep(edge + soft, edge - soft, -proj);
                return half4(0, 0, 0, alpha);
            }
            ENDHLSL
        }
    }
}