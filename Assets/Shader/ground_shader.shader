Shader "Custom/VertexColor"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_MainTex2("Albedo (RGB)", 2D) = "white"{}
		_MainTex3("Albedo (RGB)", 2D) = "white"{}
		_MainTex4("Albedo (RGB)", 2D) = "white"{}
		_Metallic("Metallic", Range(0,1)) = 0
		_Smoothness("Smoothness", Range(0,1)) = 0.5
		_BumpMap("Normalmap",2D) = "bump"{}
		//_Occlusion("Occlusion", 2D) = "white"{}

	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard
		#pragma target 3.0


		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _MainTex3;
		sampler2D _MainTex4;
		sampler2D _BumpMap;
		//sampler2D _Occlusion;
		float _Metallic;
		float _Smoothness;

	struct Input
	{
		float2 uv_MainTex;
		float2 uv_MainTex2;
		float2 uv_MainTex3;
		float2 uv_MainTex4;
		float2 uv_BumpMap;
		float4 color:COLOR;		//vertex color을 받는 변수
	};

	void surf(Input IN, inout SurfaceOutputStandard o)
	{
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		fixed4 d = tex2D(_MainTex2, IN.uv_MainTex);
		fixed4 e = tex2D(_MainTex3, IN.uv_MainTex);
		fixed4 f = tex2D(_MainTex4, IN.uv_MainTex);

		//first trial
		//o.Emission = IN.color.rgb;
		//o.Albedo = c.rgb + IN.color.rgb;				//이전 lerp 기능과 유사한 기능, 주로 파티클에서 저렴하게(?) 사용

		//second trial
		//o.Albedo = IN.color.rgb;
		o.Albedo = lerp(c.rgb, d.rgb, IN.color.r);
		o.Albedo = lerp(o.Albedo, e.rgb, IN.color.g);
		o.Albedo = lerp(o.Albedo, f.rgb, IN.color.b);

		float3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		normal.r *= IN.color.b;
		normal.g *= IN.color.b;
		o.Normal = normal;
		o.Metallic = _Metallic;
		//o.Occlusion = tex2D(_Occlusion, IN.uv_MainTex);
		//o.Smoothness = _Smoothness;
		//o.Smoothness = (IN.color.b * 0.5) *_Smoothness + 0.3;			//Specular
		o.Smoothness = 0.7f*IN.color.b;
		o.Alpha = c.a;
		}
		ENDCG
		}
	FallBack "Diffuse"
}