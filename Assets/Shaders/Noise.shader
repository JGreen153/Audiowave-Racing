Shader "Custom/Noise" {
	Properties{
		_GlowColour("Glow Colour", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "grey" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Scale("Scale", Range(0.0, 3.0)) = 0.1
		_Frequency("Glow Frequency", Float) = 1.0
		_MinPulseVal("Min Glow Multiplier", Range(0,1)) = 0.5
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows vertex:vert

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _NoiseTex;

			struct Input
			{
				float2 uv_MainTex;
			};

			half _Glossiness;
			half _Metallic;
			float _Scale;
			fixed4 _GlowColour;
			half _Frequency;
			half _MinPulseVal;

			void vert(inout appdata_full v)
			{
				half noiseVal = tex2Dlod(_NoiseTex, float4(v.texcoord.xy, 0, 0)).r;
				//v.vertex.xyz += v.normal * sin(_Time.w + noiseVal * 100) * _Scale;
				v.vertex.y += sin(_Time.w + noiseVal * 100) * _Scale;
			}

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

				half posSin = 0.5 * sin(_Frequency * _Time.x) + 0.5;
				half pulseMultiplier = posSin * (1 - _MinPulseVal) + _MinPulseVal;

				o.Albedo = c.rgb * _GlowColour.rgb * pulseMultiplier;

				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
