Shader "Custom/Wave" 
{
	Properties 
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Speed("Speed", Float) = 5
		_Scale("Scale", Float) = 20
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input 
		{
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		
		half _Glossiness;
		half _Metallic;
		
		fixed4 _RandomColour;

		float _Speed;
		float _Scale;

		float _SubBass;
		float _Bass;
		float _LowMidrange;
		float _MidHighrange;
		float _PresenceAndBrilliance;
		
		float4 GetNewVertPosition(float4 v)
		{
			const float PI = 3.14159;

			v.y += sin(_Time * _Speed + v.x) * _SubBass * _Scale * unity_DeltaTime;
			v.y += sin(_Time * _Speed + v.z) * _Bass * _Scale * unity_DeltaTime;
			v.y += sin(PI * _Time * _Speed + v.x) * sin(PI * _Time * _Speed + v.z) * _LowMidrange * _Scale * unity_DeltaTime;
			v.y += sin(PI * _Time * 10.0f + v.x * sin(PI * _Time * 10.0f + v.z)) * _MidHighrange * _Scale * unity_DeltaTime;
			v.x += sin(_Time * _Speed + v.z) * _PresenceAndBrilliance * _Scale * unity_DeltaTime;

			return v;
		}

		void vert(inout appdata_full v)
		{
			float3 bitangent = cross(v.normal, v.tangent);

			float4 position = GetNewVertPosition(v.vertex);
			float4 positionAndTangent = GetNewVertPosition(v.vertex + v.tangent * 0.01);
			float4 positionAndBitangent = GetNewVertPosition(v.vertex + float4(bitangent, 1.0f) * 0.01);

			float4 newTangent = (positionAndTangent - position);
			float4 newBitangent = (positionAndBitangent - position); 

			float3 newNormal = cross(newTangent, newBitangent);
			v.vertex = GetNewVertPosition(v.vertex);
			v.normal = newNormal;
		}


		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _RandomColour;

			o.Albedo = c.rgb;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
