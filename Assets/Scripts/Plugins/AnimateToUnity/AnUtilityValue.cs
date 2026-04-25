using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnUtilityValue
	{
		public static void LimitValue(ref float value, float min, float max)
		{
			if (value > max)
			{
				value = max;
				return;
			}
			if (value < min)
			{
				value = min;
				return;
			}
		}

		public static void LimitValue(ref int value, int min, int max)
		{
			if (value > max)
			{
				value = max;
				return;
			}
			if (value < min)
			{
				value = min;
				return;
			}
		}

		public static float GetLimitValue(float value, float min, float max)
		{
			AnUtilityValue.LimitValue(ref value, min, max);
			return value;
		}

		public static int GetLimitValue(int value, int min, int max)
		{
			AnUtilityValue.LimitValue(ref value, min, max);
			return value;
		}

		public static float GetAbsValue(float value)
		{
			if (value < 0f)
			{
				return value *= -1f;
			}
			return value;
		}

		public static float GetSign(float value)
		{
			if (value < 0f)
			{
				return -1f;
			}
			return 1f;
		}

		public static int GetDigit(float value)
		{
			float absValue = AnUtilityValue.GetAbsValue(value);
			if (absValue < 1f)
			{
				return 1;
			}
			return (int)Mathf.Log10(absValue) + 1;
		}
	}
}
