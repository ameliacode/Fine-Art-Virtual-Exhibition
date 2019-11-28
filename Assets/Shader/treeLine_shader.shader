Shader "Custom/intro_shader"
{
	Properties
	{
		_Color("Main Color",Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_AlphaTex("Alpha (RGB)", 2D) = "white" {}
		_Cutoff("Alpha cutoff",Range(0,1)) = 0.5
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }


			CGPROGRAM
			#pragma surface surf Lambert alpha
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _AlphaTex;

			struct Input
			{
				float2 uv_MainTex;
				float2 uv_AlphaTex;
			};

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 alpha = tex2D(_AlphaTex, IN.uv_AlphaTex);
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * alpha * (sin(_Time.x) + 2.0);
				o.Albedo = c.rgb;
				o.Alpha = alpha.r;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
