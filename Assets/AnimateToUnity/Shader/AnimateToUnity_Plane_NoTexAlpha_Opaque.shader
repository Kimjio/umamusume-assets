//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "AnimateToUnity/Plane_NoTexAlpha_Opaque" {
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
}
SubShader {
 Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
 Pass {
  Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
  ColorMask 0 0
  Cull Off
  Offset 0, 10
  Stencil {
   ReadMask 0
   WriteMask 0
   Pass Keep
   Fail Keep
   ZFail Keep
  }
  GpuProgramID 36194
}
}
}