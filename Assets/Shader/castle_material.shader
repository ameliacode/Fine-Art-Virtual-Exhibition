Shader "Custom/castle_material"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
       // _Glossiness ("Smoothness", Range(0,1)) = 0.5
		_BumpMap("Normalmap",2D) = "bump"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _BumpMap;
	//	float _Glossiness;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

			float3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Normal = normal;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
         //   o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
