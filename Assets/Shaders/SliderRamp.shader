Shader "Toon/Ramp Lit" 
{
	Properties
	{
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
		_ShadowColor("Shadow Color", Color) = (0.5,0.5,0.5,1)
		_DiffuseVal("_DiffuseVal",Range(0.001, 1)) = 0.3
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf ToonRamp

		sampler2D _Ramp;
		half4 _ShadowColor;
		half _DiffuseVal;

		// custom lighting function that uses a texture ramp based
		// on angle between light direction and normal
		#pragma lighting ToonRamp exclude_path:prepass
		inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
		{
			#ifndef USING_DIRECTIONAL_LIGHT
			lightDir = normalize(lightDir);
			#endif

			float diff = dot(s.Normal, lightDir);
			half normalizeDiffuse = diff *0.5 + 0.5;
			half2 normalizeDiffuseUV = normalizeDiffuse;
			half3 ramp = tex2D(_Ramp, normalizeDiffuseUV).rgb;

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;

			c.rgb += _ShadowColor.xyz * max(0.0, (1.0 - (diff * atten * 2))) *_DiffuseVal;
			c.a = s.Alpha;
			return c;
		}


		sampler2D _MainTex;
		float4 _Color;

		struct Input 
		{
			float2 uv_MainTex : TEXCOORD0;
		};

		void surf(Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		ENDCG

	}

	FallBack "Mobile/VertexLit"
}