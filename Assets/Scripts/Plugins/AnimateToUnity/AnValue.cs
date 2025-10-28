using UnityEngine;

namespace AnimateToUnity
{
    public class AnValue
    {
        public static float ObjectTimeAddValue = 0.0001f;

        public static float MinAlphaValue = 0.001f;

        public static Vector4 UVInfoDefaultValue = new(1f, 1f, 0f, 0f);

        public static Color ColorZero = new(0f, 0f, 0f, 0f);

        public static Vector2 Vector2Max = Vector2.one * float.MaxValue;

        public static Vector3 Vector3Max = Vector3.one * float.MaxValue;

        public static string AssetName = "AnimateToUnity";

        public static string GlobalDataName = "as_flGlobalData";

        public static string GlobalDataMediatorPath = "AnimateToUnity/Common/as_flGlobalDataMediator";

        public static string RootName = "MOT_root";

        public static string MotionPrefix = "MOT_";

        public static string ObjectPrefix = "OBJ_";

        public static string PlanePrefix = "PLN_";

        public static string TextPrefix = "TXT_";

        public static string ObjectOffsetName = "offset";

        public static string CloneString = "(Clone)";

        public static string NormalMeshString = "_NormalMesh";

        public static string NineSliceMeshString = "_NineSliceMesh";

        public static string CustomMeshString = "_CustomMesh";

        public static string OuterTextureString = "_OuterTexture_";

        public static string CustomTextureString = "_CustomTexture_";

        public static string StencilRefString = "_StencilRef_";

        public static string StencilCompString = "_StencilComp_";

        public static int SortOrderIntervalForObject = 1;

        public static int SortOrderIntervalForPlane = 3;

        public static int SortOrderIntervalForText = 3;

        public static float DefaultCharacterSize = 10f;

        public static float DefaultTabSize = 40f;

        public static float DefaultLinespaceOffset = 0f;

        public static int JoinWordMinNum = 2;

        public static int JoinWordMaxNum = 25;

        public static string TextMainName = "TextMain";

        public static string TextShadowName = "TextShadow";

        public static string TextOutlineName = "TextOutline";

        public static string TextIconPrefix = "<quad";

        public static string TextIconSize = "size=";

        public static string TextEmpty = "";

        public static string TextReturn = "\n";

        public static char TextHalfSpaceChar = ' ';

        public static char TextCommaChar = ',';

        public static char TextPeriodChar = '.';

        public static char TextRichBracketStartChar = '<';

        public static string TextRichBracketStart = "<";

        public static char TextRichBracketEndChar = '>';

        public static string TextRichBracketEnd = ">";

        public static string TextRichEndBracketStart = "</";

        public static string TextColorPrefix = "<color";

        public static string TextColorSuffix = "</color";

        public static string TextBoldPrefix = "<b";

        public static string TextBoldSuffix = "</b";

        public static string TextItalicPrefix = "<i";

        public static string TextItalicSuffix = "</i";

        public static string TextSizePrefix = "<size";

        public static string TextSizeSuffix = "</size";

        public static string TextSettingPrefix = "<flset";

        public static string TextSettingLineSpace = "ls=";

        public static string TextSettingAnchor = "an=";

        public static string TextSettingAlign = "al=";

        public static string TextSettingTab = "tab=";

        public static string TextSettingFit = "fit=";

        public static string TextSettingWrap = "wrap=";

        public static string TextSettingIconOffsetX = "ioffx=";

        public static string TextSettingIconOffsetY = "ioffy=";

        public static string TextSettingIconOffsetSize = "ioffs=";

        public static string TextSettingOffsetX = "poffx=";

        public static string TextSettingOffsetY = "poffy=";

        public static string ShaderNormalPath = AnValue.AssetName + "/Plane_Main_Normal";

        public static string ShaderAddPath = AnValue.AssetName + "/Plane_Main_Add";

        public static string ShaderSubPath = AnValue.AssetName + "/Plane_Main_Sub";

        public static string ShaderMultiplyPath = AnValue.AssetName + "/Plane_Main_Multiply";

        public static string ShaderHardLightPath = AnValue.AssetName + "/Plane_Main_HardLight";

        public static string ShaderInvertPath = AnValue.AssetName + "/Plane_Main_Invert";

        public static string ShaderOpaquePath = AnValue.AssetName + "/Plane_Main_Opaque";

        public static string ShaderGrayscalePath = AnValue.AssetName + "/Plane_Main_Grayscale";

        public static string ShaderMaskPath = AnValue.AssetName + "/Plane_Main_Mask";

        public static string ShaderAlphaMaskPath = AnValue.AssetName + "/Plane_Main_AlphaMask";

        public static string ShaderAlphaMaskMultiplyPath = AnValue.AssetName + "/Plane_Main_AlphaMaskMultiply";

        public static string ShaderStencilMaskPath = AnValue.AssetName + "/Plane_Main_StencilMask";

        public static string ShaderStencilAlphaMaskPath = AnValue.AssetName + "/Plane_Main_StencilAlphaMask";

        public static string ShaderObjectMaskPath = AnValue.AssetName + "/Plane_Main_ObjectMask";

        public static string ShaderObjectAlphaMaskPath = AnValue.AssetName + "/Plane_Main_ObjectAlphaMask";

        public static string ShaderNormal3DPath = AnValue.AssetName + "/Plane_Main_Normal_3D";

        public static string ShaderAdd3DPath = AnValue.AssetName + "/Plane_Main_Add_3D";

        public static string ShaderEditorPath = AnValue.AssetName + "/Plane_Main_Editor";

        public static string ShaderNormalBlurPath = AnValue.AssetName + "/Plane_Main_Normal_Blur";

        public static string ShaderAddBlurPath = AnValue.AssetName + "/Plane_Main_Add_Blur";

        public static string ShaderMultiplyBlurPath = AnValue.AssetName + "/Plane_Main_Muliply_Blur";

        public static string ShaderGrayscaleBlurPath = AnValue.AssetName + "/Plane_Main_Grayscale_Blur";

        public static string ShaderNormalHorizontalFadePath = AnValue.AssetName + "/Plane_Main_Normal_HorizontalFade";

        public static string ShaderNormalVerticalFadePath = AnValue.AssetName + "/Plane_Main_Normal_VerticalFade";

        public static string ShaderMainString = "_Main";

        public static string ShaderA8String = "_A8";

        public static string ShaderNoTexAlphaString = "_NoTexAlpha";

        public static string ShaderHorizontalFadeString = "_HorizontalFade";

        public static string ShaderVerticalFadeString = "_VerticalFade";

        public static string ShaderTextNormalPath = AnValue.AssetName + "/Text_Main_Normal";

        public static string ShaderTextNormalGradationPath = AnValue.AssetName + "/Text_Main_Normal_Gradation";

        public static string ShaderTextAddPath = AnValue.AssetName + "/Text_Main_Add";

        public static string ShaderTextAddGradationPath = AnValue.AssetName + "/Text_Main_Add_Gradation";

        public static string ShaderTextMultiplyPath = AnValue.AssetName + "/Text_Main_Multiply";

        public static string ShaderTextMultiplyGradationPath = AnValue.AssetName + "/Text_Main_Multiply_Gradation";

        public static string ShaderTextGrayscalePath = AnValue.AssetName + "/Text_Main_Grayscale";

        public static string ShaderTextGrayscaleGradationPath = AnValue.AssetName + "/Text_Main_Grayscale_Gradation";

        public static string ShaderTextStencilAlphaMaskPath = AnValue.AssetName + "/Text_Main_StencilAlphaMask";

        public static string ShaderTextIconNormalPath = AnValue.AssetName + "/Text_Icon_Normal";

        public static string ShaderTextIconAddPath = AnValue.AssetName + "/Text_Icon_Add";

        public static string ShaderTextIconMultiplyPath = AnValue.AssetName + "/Text_Icon_Multiply";

        public static string ShaderTextIconGrayscalePath = AnValue.AssetName + "/Text_Icon_Grayscale";

        public static string ShaderParamMainTex = "_MainTex";

        public static string ShaderParamAlphaTex = "_AlphaTex";

        public static string ShaderParamMultiplyColor = "_multiplyColor";

        public static string ShaderParamColorOffset = "_colorOffset";

        public static string ShaderParamOffset = "_offset";

        public static string ShaderParamUVColorInfo = "_uvColorInfo";

        public static string ShaderParamUVAlphaInfo = "_uvAlphaInfo";

        public static string ShaderParamStencilRef = "_Stencil";

        public static string ShaderParamStencilComp = "_StencilComp";

        public static string ShaderParamBlurOffsetX = "_blurOffsetX";

        public static string ShaderParamBlurOffsetY = "_blurOffsetY";

        public static string ShaderParamBlurQuality = "_blurQuality";

        public static string ShaderParamBlurOffsetListX = "_blurOffsetListX";

        public static string ShaderParamBlurOffsetListY = "_blurOffsetListY";

        public static string ShaderParamBlurWeightList = "_blurWeightList";

        public static string ShaderParamGradationStartColor = "_gradStartColor";

        public static string ShaderParamGradationEndColor = "_gradEndColor";

        public static string ShaderParamGradationStartPosition = "_gradStartPosition";

        public static string ShaderParamGradationEndPosition = "_gradEndPosition";

        public static string ShaderParamAlphaFadeParam = "_AlphaParam";

        public static string TextureColorSuffix = "_C";

        public static string TextureAlphaSuffix = "_A";

        public static string CustomModelDirName = "_FlModel";

        public static string PrimitiveMeshRootPath = "FLATOUT/PrimitiveMesh";

        public static string PrimitiveMeshCubePath = AnValue.PrimitiveMeshRootPath + "pf_cube";

        public static string PrimitiveMeshCylinderPath = AnValue.PrimitiveMeshRootPath + "pf_cylinder";

        public static string PrimitiveMeshRingPath = AnValue.PrimitiveMeshRootPath + "pf_ring";

        public static string PrimitiveMeshSpherePath = AnValue.PrimitiveMeshRootPath + "pf_sphere";

        public static string PrimitiveMeshPlanePath = AnValue.PrimitiveMeshRootPath + "pf_plane";
    }
}
