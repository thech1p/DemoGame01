Shader "Custom/ColoredPoint"
{
    Properties
    {
        //_Color ("Color", Color) = (1,1,1,1)
        //_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _InputFactor("Input", Range(0,2)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        //sampler2D _MainTex;

        struct Input
        {
            float3 worldPos;
            //float2 uv_MainTex;
        };
        half _InputFactor;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo.rgb = (IN.worldPos.xyz*0.5+0.5)*_InputFactor;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1*_InputFactor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
