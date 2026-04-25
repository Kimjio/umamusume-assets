using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnUtilityColor
	{
		public static bool IsSameColor(Color src, Color dst)
		{
			return src.a == dst.a && src.r == dst.r && src.b == dst.b && src.g == dst.g;
		}

		public static void AddColor(ref Color src, Color dst)
		{
			src.r += dst.r;
			src.g += dst.g;
			src.b += dst.b;
			src.a += dst.a;
		}

		public static void MultiplyColor(ref Color src, Color dst)
		{
			src.r *= dst.r;
			src.g *= dst.g;
			src.b *= dst.b;
			src.a *= dst.a;
		}

		public static Color[] CreateColorList(int count, Color defautlValue)
		{
			Color[] array = new Color[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = defautlValue;
			}
			return array;
		}
	}
}
