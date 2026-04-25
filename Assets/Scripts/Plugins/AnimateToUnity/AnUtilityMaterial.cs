using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnUtilityMaterial
	{
		public static AnShaderTypes GetShaderTypeFromBlend(AnBlendModeTypes blendModeType)
		{
			return AnUtilityMaterial.GetShaderTypeFromBlendBase(blendModeType, false, false);
		}

		public static AnShaderTypes GetShaderTypeFromBlend(AnBlendModeTypes blendModeType, bool blur, bool gradation)
		{
			return AnUtilityMaterial.GetShaderTypeFromBlendBase(blendModeType, blur, gradation);
		}

		private static AnShaderTypes GetShaderTypeFromBlendBase(AnBlendModeTypes blendModeType, bool blur, bool gradation)
		{
			if (!blur && !gradation)
			{
				switch (blendModeType)
				{
				case AnBlendModeTypes.Normal:
					return AnShaderTypes.Normal;
				case AnBlendModeTypes.Add:
					return AnShaderTypes.Add;
				case AnBlendModeTypes.Sub:
					return AnShaderTypes.Sub;
				case AnBlendModeTypes.Multiply:
					return AnShaderTypes.Multiply;
				case AnBlendModeTypes.HardLight:
					return AnShaderTypes.HardLight;
				case AnBlendModeTypes.Invert:
					return AnShaderTypes.Invert;
				default:
					return AnShaderTypes.Normal;
				}
			}
			else
			{
				if (blur && !gradation)
				{
					switch (blendModeType)
					{
					case AnBlendModeTypes.Normal:
						return AnShaderTypes.NormalBlur;
					case AnBlendModeTypes.Add:
						return AnShaderTypes.AddBlur;
					case AnBlendModeTypes.Multiply:
						return AnShaderTypes.MultiplyBlur;
					}
					return AnShaderTypes.NormalBlur;
				}
				if (gradation && !blur)
				{
					switch (blendModeType)
					{
					case AnBlendModeTypes.Normal:
						return AnShaderTypes.NormalGradation;
					case AnBlendModeTypes.Add:
						return AnShaderTypes.AddGradation;
					case AnBlendModeTypes.Multiply:
						return AnShaderTypes.MultiplyGradation;
					}
					return AnShaderTypes.NormalGradation;
				}
				return AnShaderTypes.Normal;
			}
		}

		public static AnStencilCompareFuncTypes GetStencilCompareType(AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes baseStencilCompareType)
		{
			if (shaderType == AnShaderTypes.StencilAlphaMask || shaderType == AnShaderTypes.StencilMask)
			{
				return AnStencilCompareFuncTypes.Always;
			}
			if (stencilRef != baseStencilRef)
			{
				return AnStencilCompareFuncTypes.Equal;
			}
			if (baseStencilCompareType == AnStencilCompareFuncTypes.None)
			{
				return AnStencilCompareFuncTypes.Disabled;
			}
			return baseStencilCompareType;
		}

		public static string GetMaterialKey(AnShaderTypes shaderType, int stencilRef, AnStencilCompareFuncTypes stencilCompareType)
		{
			if (stencilRef == 0 && stencilCompareType == AnStencilCompareFuncTypes.Disabled)
			{
				return shaderType.ToString();
			}
			return string.Concat(new string[]
			{
				shaderType.ToString(),
				AnValue.StencilRefString,
				stencilRef.ToString(),
				AnValue.StencilCompString,
				stencilCompareType.ToString()
			});
		}

		public static void ComputeGaussianBlurList(float quality, float amount, int precision, ref float[] offsetListX, ref float[] offsetListY, ref float[] weightList)
		{
			float num = quality * 2f + 1f;
			float num2 = num * num;
			if (offsetListX == null)
			{
				offsetListX = new float[49];
				offsetListY = new float[49];
				weightList = new float[49];
			}
			float num3 = quality / amount;
			float num4 = 2f * num3 * num3;
			float num5 = Mathf.Sqrt(num4 * 3.1415927f);
			float num6 = 0f;
			int num7 = 0;
			int num8 = 0;
			while ((float)num8 < num2)
			{
				float num9 = ((float)num8 % num - quality) * 1f;
				float num10 = ((float)((int)((float)num8 / num)) - quality) * 1f;
				if (precision == 1)
				{
					if (num9 == 0f || num10 == 0f)
					{
						goto IL_00C0;
					}
				}
				else if (precision != 2 || AnUtilityValue.GetAbsValue(num9) == AnUtilityValue.GetAbsValue(num10) || num9 == 0f || num10 == 0f)
				{
					goto IL_00C0;
				}
				IL_010B:
				num8++;
				continue;
				IL_00C0:
				offsetListX[num7] = num9;
				offsetListY[num7] = num10;
				weightList[num7] = Mathf.Exp(-(offsetListX[num8] * offsetListX[num8] + offsetListY[num8] * offsetListY[num8]) / num4) / num5;
				num6 += weightList[num7];
				num7++;
				goto IL_010B;
			}
			int num11 = 0;
			while ((float)num11 < num2)
			{
				weightList[num11] /= num6;
				num11++;
			}
		}
	}
}
