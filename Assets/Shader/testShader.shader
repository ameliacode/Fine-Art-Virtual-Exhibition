Shader "Custom/testShader"
{
    Properties
    {
        _MainTex1("Albedo (RGB)", 2D) = "white" {}
		_MainTex2("Albedo (RGB)", 2D) = "white" {}
		_MainTex3("Albedo (RGB)", 2D) = "white" {}
		_MainTex4("Albedo (RGB)", 2D) = "white" {}

		_BumpTex("Normal",2D)="bump"{}
       
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex1;
	    sampler2D _MainTex2;
		sampler2D _MainTex3;
		sampler2D _MainTex4;
		sampler2D _BumpTex;

        struct Input
        {
            float2 uv_MainTex1;
			float2 uv_MainTex2;
			float2 uv_MainTex3;
			float2 uv_MainTex4;
			float2 uv_BumpTex;
			float4 color:COLOR;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 c_1 = tex2D(_MainTex1, IN.uv_MainTex1);
			fixed4 c_2 = tex2D(_MainTex2, IN.uv_MainTex2);
			fixed4 c_3 = tex2D(_MainTex3, IN.uv_MainTex3);
			fixed4 c_4 = tex2D(_MainTex4, IN.uv_MainTex4);

			o.Albedo = c_1.rgb;
			o.Albedo = lerp(o.Albedo, c_2, IN.color.r);
			o.Albedo = lerp(o.Albedo, c_3, IN.color.g);
			o.Albedo = lerp(o.Albedo, c_4, IN.color.b);

			float3 n = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
			n.r *= IN.color.b;
			n.g *= IN.color.b;
			o.Normal = n;
			o.Smoothness = 0.7f*IN.color.b;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
