// Simplified Bumped Specular shader. Differences from regular Bumped Specular one:
// - no Main Color nor Specular Color
// - specular lighting directions are approximated per vertex
// - writes zero to alpha channel
// - Normalmap uses Tiling/Offset of the Base texture
// - no Deferred Lighting support
// - no Lightmap support
// - supports ONLY 1 directional light. Other lights are completely ignored.

Shader "Mobile/Bumped Specular Colored (1 Directional Light)" {
	Properties{
		_Shininess("Shininess", Range(0.03, 1)) = 0.17
		_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_Gloss("Gloss",Range(0.001, 1)) = 0.3
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 250

		CGPROGRAM
		#pragma surface surf MobileBlinnPhong exclude_path:prepass nolightmap noforwardadd halfasview //novertexlights

		inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
		{
			fixed diff = max(0, dot(s.Normal, lightDir * 1.8));
			fixed nh = max(0, dot(s.Normal, halfDir ));
			fixed spec = pow(nh, s.Specular * 256 ) * s.Gloss;

			fixed4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
			c.a = 0.0;
			return c;
		}

		sampler2D _MainTex;
		half _Shininess;
		half _Gloss;
		half4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			tex *= _Color;
			o.Albedo = tex.rgb;
			o.Gloss = _Gloss;
			o.Alpha = tex.a;
			o.Specular = _Shininess;
		}
		ENDCG
	}

		FallBack "Mobile/VertexLit"
}