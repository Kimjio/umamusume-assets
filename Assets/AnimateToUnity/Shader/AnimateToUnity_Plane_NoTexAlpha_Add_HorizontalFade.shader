//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "AnimateToUnity/Plane_NoTexAlpha_Add_HorizontalFade" {
Properties {
_MainTex ("Color Texture", 2D) = "white" { }
_uvColorInfo ("_uvColorInfo", Vector) = (1,1,0,0)
_uvAlphaInfo ("_uvAlphaInfo", Vector) = (1,1,0,0)
_StencilComp ("Stencil Comparison", Float) = 8
_Stencil ("Stencil ID", Float) = 0
_StencilOp ("Stencil Operation", Float) = 0
_StencilWriteMask ("Stencil Write Mask", Float) = 255
_StencilReadMask ("Stencil Read Mask", Float) = 255
_ColorMask ("Color Mask", Float) = 15
_AlphaParam ("AlphaParam", Vector) = (0,0,0,0)
}
SubShader {
 Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
 Pass {
  Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
  Blend SrcAlpha One, Zero One
  ColorMask 0 0
  ZWrite Off
  Cull Off
  Stencil {
   ReadMask 0
   WriteMask 0
   Pass Keep
   Fail Keep
   ZFail Keep
  }
  GpuProgramID 64309
}
}
}