// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
	}

	SubShader {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent"}

		Pass {
			ZWrite Off
			ZTest Greater
			Lighting Off
			Color [_Color]
		}

	}
	FallBack "Diffuse"
}
