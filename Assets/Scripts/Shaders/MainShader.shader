Shader "Custom/MainShader"
{
    Properties
    {
        [HideInInspector] _PlayerPos ("PlayerPosition", Vector) = (0,0,0,0)
        [HideInInspector] _Radius ("Radius", float) = 0.0
        [HideInInspector] _SideRatio("SideRatio", float) = 1
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Feather("Feather", float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;
        };

        half _Radius;
        half _Feather;
        fixed2 _PlayerPos;
        fixed _SideRatio;

        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            fixed2 screenUV = IN.screenPos.xy / IN.screenPos.w;
            screenUV.x *= _SideRatio;
            _PlayerPos.x *= _SideRatio;
            half dist = distance(screenUV, _PlayerPos);
            half mask = smoothstep(_Radius, _Radius + _Feather, dist);
            o.Albedo = c.rgb * (1 - mask);
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
