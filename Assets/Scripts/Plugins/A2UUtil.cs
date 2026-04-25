using System;
using System.Text;
using UnityEngine;

public static class A2UUtil
{
	public static void SetLayer(int layer, Transform trans)
	{
		if (trans == null)
		{
			return;
		}
		trans.gameObject.layer = layer;
		for (int i = 0; i < trans.childCount; i++)
		{
			A2UUtil.SetLayer(layer, trans.GetChild(i));
		}
	}

	public static class FNVHash
	{
		public static int Generate(string seed)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(seed);
			uint num = A2UUtil.FNVHash.FNV_OFFSET_BASIS_32;
			for (int i = 0; i < bytes.Length; i++)
			{
				num = (A2UUtil.FNVHash.FNV_PRIME_32 * num) ^ (uint)bytes[i];
			}
			return (int)num;
		}

		private static uint FNV_OFFSET_BASIS_32 = 2166136261U;

		private static uint FNV_PRIME_32 = 16777619U;
	}
}
