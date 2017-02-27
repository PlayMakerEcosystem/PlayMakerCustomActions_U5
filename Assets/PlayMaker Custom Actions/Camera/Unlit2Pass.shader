Shader "Custom/Unlit2Pass" {

Properties {
					_Color ("Main Color", Color) = (1,1,1,1)
					_Tex1 ("Base", Rect) = "white" {}
					_Tex2 ("Base", Rect) = "white" {}
			} 

	Category {
					ZWrite Off Alphatest Greater 0 ColorMask RGB Lighting Off
					Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
					Blend SrcAlpha OneMinusSrcAlpha

	SubShader {
	Pass {SetTexture [_Tex2]}
	Pass {SetTexture [_Tex1] {constantColor [_Color] Combine texture * constant, texture * constant}}
			}
			}


	FallBack "Diffuse"
}
