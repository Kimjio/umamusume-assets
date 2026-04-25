using System;
using UnityEngine;

public class SafeAreaResolver
{
	public static void SetResolutionScale(float scale = 1f)
	{
	}

	public static Rect SafeArea
	{
		get
		{
			return Screen.safeArea;
		}
	}
}
