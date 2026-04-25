//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "AnimateToUnity/Text_Main_Add_Gradation" {
Properties {
_MainTex ("Color Texture", 2D) = "white" { }
_multiplyColor ("_multiplyColor", Color) = (1,1,1,1)
_colorOffset ("_colorOffset", Color) = (0,0,0,0)
_StencilComp ("Stencil Comparison", Float) = 8
_Stencil ("Stencil ID", Float) = 0
_StencilOp ("Stencil Operation", Float) = 0
_StencilWriteMask ("Stencil Write Mask", Float) = 255
_StencilReadMask ("Stencil Read Mask", Float) = 255
_ColorMask ("Color Mask", Float) = 15
_gradStartColor ("_gradStartColor", Color) = (1,1,1,1)
_gradEndColor ("_gradEndColor", Color) = (1,1,1,1)
_gradStartPosition ("_gradStartColorPosition", Vector) = (0,0,0,0)
_gradEndPosition ("_gradEndColorPosition", Vector) = (0,0,0,0)
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
  GpuProgramID 17388
}
}
}